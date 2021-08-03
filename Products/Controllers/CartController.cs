using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CartController(IRepositoryManager repository, ILoggerManager logger,
            UserManager<User> userManager, IMapper mapper)
        {
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary> Get products from a user basket </summary>
        /// <returns> Products from a user basket</returns>
        [HttpGet(Name = "GetUserCart")]
        [Authorize]
        public async Task<IActionResult> GetUserCart()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var productsAndAmount = await _repository.Cart.GetUserCarts(user);

            var productsDto = _mapper.Map<IEnumerable<ProductOutgoingDto>>(productsAndAmount.Item1);
            var totalAmount = productsAndAmount.Item2;

            return Ok(new { totalAmount = totalAmount, products = productsDto });
        }

        /// <summary> Add product to user basket </summary>
        /// <param name="productId"></param>
        /// <returns>No content</returns>
        [HttpPost("{productId}", Name = "AddProductToBasket")]
        [Authorize]
        public async Task<IActionResult> AddProductToBasket(int productId)
        {
            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogError($"Products with id: {productId} doesn't exist in database");
                return NotFound();
            }

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var cart = new Cart { UserId = user.Id,ProductId = product.Id};

            _repository.Cart.CreateCart(cart);

            await _repository.SaveAsync();

            return Ok();
        }


        /// <summary> Delete user after authenticate </summary>
        /// <param name="productId"></param>
        /// <returns>No content</returns>
        [HttpDelete(Name = "DeleteCarts")]
        [Authorize]
        public async Task<IActionResult> DeleteCarts(int productId)
        {
            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogError($"Products with id: {productId} doesn't exist in database");
                return NotFound();
            }
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var cart = new Cart { User = user, Product = product };

            _repository.Cart.DeleteCart(cart);

            return NoContent();
        }
    }
}
