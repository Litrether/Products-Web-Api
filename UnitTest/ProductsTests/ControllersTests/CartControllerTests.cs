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
            store.Setup(x => x.FindByIdAsync("123", CancellationToken.None))
                .ReturnsAsync(new User()
                {
                    UserName = "Name",
                    Id = "111"
                });
            _userManager = new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);

            var identity = new GenericIdentity("Name", "test");
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
                false, 0).Result).Returns(products);

            var result = await _controller.GetCartProducts(productParams);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsBadRequestWhenCartExist()
        {
            var testProductId = 1;
            var cart = new Cart
            {
                Id = 1,
                Product = null,
                ProductId = testProductId,
                User = null,
                UserId = "111"
            };
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                testProductId, false).Result).Returns(cart);

            var result = await _controller.CreateCartProduct(testProductId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsBadRequestWhenExceptionSave()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().First();
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                It.IsAny<int>(), It.IsAny<bool>()).Result).Returns(null as Cart);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0).Result).Returns(product);
            _repo.Setup(repo => repo.SaveAsync()).Throws(new Exception("Test message", new Exception("Test inner message")));

            var result = await _controller.CreateCartProduct(testProductId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void CreateCartProductsReturnsOkResult()
        {
            var testProductId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            _repo.Setup(repo => repo.Cart.GetCartProductAsync(It.IsAny<User>(),
                testProductId, false).Result).Returns(null as Cart);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0).Result).Returns(product);

            var result = await _controller.CreateCartProduct(testProductId);

            Assert.IsType<OkObjectResult>(result);
        }

    }
}
