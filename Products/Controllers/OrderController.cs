using AutoMapper;
using Contracts;
using CrossCuttingLayer;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class OrderController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IBus _bus;
        Uri uri;

        public OrderController(IBus bus, IRepositoryManager repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
            _bus = bus;
            uri = new Uri("rabbitmq://localhost/orderQueue");
        }

        /// <summary> Send message for creating new order </summary>
        /// <param name="productId"></param>
        /// <returns> Message about successfull sending query </returns>
        [HttpPost("{productId}")]
        [Authorize(Roles = ("Administrator, Manager"))]
        public async Task<IActionResult> PostOrder(int productId)
        {
            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogError($"Product with id: {productId} doesn't exist in database.");
                return BadRequest($"Product with id: {productId} doesn't exist in database.");
            }

            var message = new OrderMessage()
            {
                Cost = product.Cost,
                ProductId = productId,
                Username = User.Identity.Name,
                Function = CrossFunction.POST,
            };

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);

            return Ok("The order is accepted for processing.");
        }

        /// <summary> Send message for deleting order </summary>
        /// <param name="id"></param>
        /// <returns> Message about successfull sending query </returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = ("Administrator, Manager"))]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var message = new OrderMessage() { Id = id, Function = CrossFunction.DELETE };

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);

            return Ok("The query is accepted for processing.");
        }

        /// <summary> Send message for update order status </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns> Message about successfull sending query </returns>
        [HttpPut("{id}")]
        [Authorize(Roles = ("Administrator, Manager"))]
        public async Task<IActionResult> UpdateOrderStatus(int id, string status)
        {
            if (status != "Processed" && status != "On the way" && status != "Delivered")
                return BadRequest("Status must has value: 'Processed','On the way' or 'Delivered'");

            var message = new OrderMessage() {
                Id = id,
                Status = status,
                Function = CrossFunction.PUT,
            };

            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(message);

            return Ok("The query is accepted for processing.");
        }
    }
}
