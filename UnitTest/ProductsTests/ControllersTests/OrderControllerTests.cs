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
        public async void AddOrderReturnsBadRequestObjectResultWhenProductNotFound()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            _repo.Setup(repo => repo.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(),
                It.IsAny<int>())).ReturnsAsync(value: null);

            var result = await _controller.PostOrder(0);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void AddOrderReturnsOkObjectResult()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testId = 1;
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testId, false, 0)).ReturnsAsync(product);
            _bus.Setup(bus => bus.GetSendEndpoint(_uri)).ReturnsAsync(sendEndpoint.Object);

            var result = await _controller.PostOrder(testId);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UpdateOrderStatusReturnsOkObjectResult()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testId = 1;
            var status = "Processed";
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);
            _repo.Setup(repo => repo.Product.GetProductAsync(testId, false, 0)).ReturnsAsync(product);
            _bus.Setup(bus => bus.GetSendEndpoint(_uri)).ReturnsAsync(sendEndpoint.Object);

            var result = await _controller.UpdateOrderStatus(testId, status);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void UpdateOrderStatusReturnsBadRequestObjectResultWhenStatusInvalid()
        {
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            var testId = 1;
            var status = "NotExistedStatus";
            var product = EntitiesForTests.Products().ToList().First(p => p.Id == testId);

            var result = await _controller.UpdateOrderStatus(testId, status);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async void DeleteOrderReturnsOkObjectResult()
        {
            var testOrderId = 1;
            Mock<ISendEndpoint> sendEndpoint = new Mock<ISendEndpoint>();
            _bus.Setup(bus => bus.GetSendEndpoint(_uri)).ReturnsAsync(sendEndpoint.Object);

            var result = await _controller.DeleteOrder(testOrderId);

            Assert.IsType<OkObjectResult>(result);
        }
    }
}
