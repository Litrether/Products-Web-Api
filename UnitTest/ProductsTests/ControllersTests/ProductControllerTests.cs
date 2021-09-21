using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using UnitTestProducts;
using Xunit;

namespace UnitTests.ProductsTests.ControllersTests
{
    public class ProductControllerTests
    {
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<IMapper> _mapper = new Mock<IMapper>();
        Mock<IDataShaper<ProductOutgoingDto>> _dataShaper = new Mock<IDataShaper<ProductOutgoingDto>>();
        Mock<ICurrencyApiConnection> _currencyConnection = new Mock<ICurrencyApiConnection>();
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _controller = new ProductController(_repo.Object, _logger.Object, _mapper.Object, _dataShaper.Object, _currencyConnection.Object);
        }

        [Fact]
        public async void GetProductReturnsBadRequestObjectResultWhenCostRangeInvalid()
        {
            var productParams = new ProductParameters()
            {
                MinCost = 10,
                MaxCost = 0,
            };

            var result = await _controller.GetProduct(-1, productParams);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void GetProductReturnsOkObjectResultWhenProductExist()
        {
            var testProductId = 1;
            var testProduct = EntitiesForTests.Products().First();

            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0).Result)
                .Returns(testProduct);
            var productParams = new ProductParameters();

            var result = await _controller.GetProduct(testProductId, productParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetProductsReturnsOkObjectResult()
        {
            var productParams = new ProductParameters();
            var products = PagedList<Product>
                .ToPagedList(EntitiesForTests.Products(), productParams.PageNumber, productParams.PageSize);
            _repo.Setup(repo => repo.Product.GetAllProductsAsync(productParams, false, 0).Result)
                .Returns(products);

            var result = await _controller.GetProducts(productParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetProductsReturnsNotFoundResultWhenProductsEmpty()
        {
            var productParams = new ProductParameters();
            var emptyProductList = new List<Product>();
            var products = PagedList<Product>
                .ToPagedList(emptyProductList, productParams.PageNumber, productParams.PageSize);
            _repo.Setup(repo => repo.Product.GetAllProductsAsync(productParams, false, 0).Result)
                .Returns(products);

            var result = await _controller.GetProducts(productParams);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void AddProductsReturnsRedirectToRouteResult()
        {
            var product = EntitiesForTests.Products().First();
            var productIncomingDto = MapProductToProductIncomingDto(product);
            var productOutgoingDto = MapProductToProductOutgoingDto(product);
            _mapper.Setup(map => map.Map<Product>(productIncomingDto)).Returns(product);
            _mapper.Setup(map => map.Map<ProductOutgoingDto>(product)).Returns(productOutgoingDto);
            _repo.Setup(repo => repo.Product.CreateProduct(product)).Verifiable();

            var result = await _controller.CreateProduct(productIncomingDto);

            var redirectToRouteResult = Assert.IsType<RedirectToRouteResult>(result);
            Assert.Equal("GetProduct", redirectToRouteResult.RouteName);
            _repo.Verify(repo => repo.Product.CreateProduct(It.IsAny<Product>()));
        }

        [Fact]
        public async void UpdateProductReturnsNoContentResult()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().First();
            var productIncomingDto = MapProductToProductIncomingDto(product);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, true, 0)).ReturnsAsync(product);

            var result = await _controller.UpdateProduct(testProductId, productIncomingDto);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void UpdateProductReturnsBadRequestObjectResultWhenExceptionSave()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().First();
            var productIncomingDto = MapProductToProductIncomingDto(product);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, true, 0)).ReturnsAsync(product);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.UpdateProduct(testProductId, productIncomingDto);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteProductReturnsNoContentResult()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0)).ReturnsAsync(product);
            _repo.Setup(repo => repo.Product.DeleteProduct(product)).Verifiable();

            var result = await _controller.DeleteProduct(testProductId);

            Assert.IsType<NoContentResult>(result);
            _repo.Verify(repo => repo.Product.DeleteProduct(It.IsAny<Product>()));
        }

        [Fact]
        public async void DeleteProductReturnsBadRequestObjectResultWhenExceptionSave()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            var productIncomingDto = MapProductToProductIncomingDto(product);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, true, 0)).ReturnsAsync(product);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.DeleteProduct(testProductId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        private ProductIncomingDto MapProductToProductIncomingDto(Product product)
        {
            return new ProductIncomingDto
            {
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                ProviderId = product.ProviderId,
            };
        }

        private ProductOutgoingDto MapProductToProductOutgoingDto(Product product)
        {
            return new ProductOutgoingDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Cost = product.Cost,
                ImageUrl = product.ImageUrl,
                CategoryId = product.CategoryId,
                Category = null,
                ProviderId = product.ProviderId,
                Provider = null,
            };
        }
    }
}
