using AutoMapper;
using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Controllers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using UnitTestProducts;
using Xunit;

namespace UnitTests.ProductsTests.ControllersTests
{
    public class CartControllerTests
    {
        Mock<IMapper> _mapper = new Mock<IMapper>();
        Mock<ICurrencyApiConnection> _currency = new Mock<ICurrencyApiConnection>();
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        UserManager<User> _userManager;
        private readonly CartController _controller;

        public CartControllerTests()
        {
            var store = new Mock<IUserStore<User>>();
            store.Setup(x => x.FindByNameAsync("UserName", CancellationToken.None))
                .ReturnsAsync(new User()
                {
                    UserName = "UserName",
                    Id = "7c8790f8-2d7e-4229-be67-a373d8ceaeb7"
                });
            _userManager = new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);

            var identity = new GenericIdentity("UserName", "test");
            var contextUser = new ClaimsPrincipal(identity);
            var controllerContext = new ControllerContext()
            { HttpContext = new DefaultHttpContext { User = contextUser } };

            _controller = new CartController(_currency.Object, _repo.Object, _logger.Object,
                _userManager, _mapper.Object)
            {
                ControllerContext = controllerContext,
            };
        }

        [Fact]
        public async void GetCartProductsReturnsOkResult()
        {
            var productParams = new ProductParameters();
            var products = PagedList<Product>
                .ToPagedList(EntitiesForTests.Products(), productParams.PageNumber, productParams.PageSize);
            _repo.Setup(repo => repo.Cart.GetCartProductsAsync(productParams, It.IsAny<User>(),
                false, 0)).ReturnsAsync(products);

            var result = await _controller.GetCartProducts(productParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsBadRequestObjectResultWhenCartExist()
        {
            var testId = 1;
            var cart = new Cart
            {
                Id = 1,
                Product = null,
                ProductId = testId,
                User = null,
                UserId = "111"
            };
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                testId, false)).ReturnsAsync(cart);

            var result = await _controller.CreateCartProduct(testId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsBadRequestWhenExceptionSave()
        {
            var user = new User()
            {
                UserName = "UserName",
                Id = "7c8790f8-2d7e-4229-be67-a373d8ceaeb7"
            };
            var testId = 1;
            var product = EntitiesForTests.Products().First();
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(user,
                It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            _repo.Setup(repo => repo.Product.GetProductAsync(testId, false, 0)).ReturnsAsync(product);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

           var result = await _controller.CreateCartProduct(testId);

           Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsOkResult()
        {
            var user = new User()
            {
                UserName = "UserName",
                Id = "7c8790f8-2d7e-4229-be67-a373d8ceaeb7"
            };
            var testId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testId, false, 0)).ReturnsAsync(product); 
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(user,
                It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);

            var result = await _controller.CreateCartProduct(testId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void DeleteCartProductsReturnsNoContent()
        {
            var testId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            var cart = new Cart
            {
                Id = 1,
                Product = null,
                ProductId = testId,
                User = null,
                UserId = "111"
            };
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                testId, false)).ReturnsAsync(cart);

            var result = await _controller.DeleteCartProduct(testId);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async void DeleteCartProductsReturnsNotFoundWhenCartNotExist()
        {
            var user = new User()
            {
                UserName = "UserName",
                Id = "7c8790f8-2d7e-4229-be67-a373d8ceaeb7"
            };
            var testId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(user,
                testId, false)).ReturnsAsync(value: null);

            var result = await _controller.DeleteCartProduct(testId);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void DeleteCartProductsReturnsBadRequestObjectResultWhenExceptionSave()
        {
            var testId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            var cart = new Cart
            {
                Id = 1,
                Product = null,
                ProductId = testId,
                User = null,
                UserId = "111"
            };
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                testId, false)).ReturnsAsync(cart);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.DeleteCartProduct(testId);

            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
