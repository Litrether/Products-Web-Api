using Contracts;
using Entities.Models;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Products.Controllers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using UnitTestProducts;
using Xunit;

namespace UnitTests.ProductsTests.ControllersTests
{
    public class OrderControllerTests
    {
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<IBus> _bus = new Mock<IBus>();
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        private readonly OrderController _controller;
        private readonly Uri _uri;

        public OrderControllerTests()
        {
            var identity = new GenericIdentity("Name", "test");
            var contextUser = new ClaimsPrincipal(identity);

            var controllerContext = new ControllerContext()
            { HttpContext = new DefaultHttpContext { User = contextUser } };


            _uri = new Uri("rabbitmq://localhost/orderQueue");
            _controller = new OrderController(_bus.Object, _repo.Object, _logger.Object)
            {
                ControllerContext = controllerContext,
            };
        }

        [Fact]
        public async void AddOrderReturnsBadRequestWhenProductNotExist()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testProductId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, It.IsAny<int>()).Result).Returns(null as Product);

            var result = await _controller.PostOrder(testProductId);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void AddOrderReturnsOkObjectResult()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testProductId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0).Result).Returns(product);
            _bus.Setup(bus => bus.GetSendEndpoint(_uri).Result).Returns(sendEndpoint.Object);

            var result = await _controller.PostOrder(testProductId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UpdateOrderStatusReturnsOkObjectResult()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testProductId = 1;
            var status = "Processed";
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testProductId, false, 0).Result).Returns(product);
            _bus.Setup(bus => bus.GetSendEndpoint(_uri).Result).Returns(sendEndpoint.Object);

            var result = await _controller.UpdateOrderStatus(testProductId, status);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UpdateOrderStatusReturnsBadRequestWhenStatusInvalid()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testProductId = 1;
            var status = "NotExistedStatus";
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testProductId);

            var result = await _controller.UpdateOrderStatus(testProductId, status);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteOrderReturnsOkObjectResult()
        {
            var testOrderId = 1;
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            _bus.Setup(bus => bus.GetSendEndpoint(_uri).Result).Returns(sendEndpoint.Object);

            var result = await _controller.DeleteOrder(testOrderId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
