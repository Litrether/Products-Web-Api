using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Outcoming;
using Entities.DataTransferObjects.Outgoing;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Linq;
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
        [HttpGet(Name = "GetCartProducts")]
        [Authorize]
        public async Task<IActionResult> GetCartProducts()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var products = await _repository.Cart.GetCartProducts(user);

            var productsDto = _mapper.Map<IEnumerable<ProductOutgoingDto>>(products);

            return Ok(productsDto);
        }

        /// <summary> Add product to user basket </summary>
        /// <param name="productId"></param>
        /// <returns>No content</returns>
        [HttpPost("{productId}", Name = "CreateCartProduct")]
        [Authorize]
        [ServiceFilter(typeof(ValidateCartAttribute))]
        public async Task<IActionResult> CreateCartProduct(int productId)
        {
            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = new Cart { UserId = user.Id, ProductId = product.Id};

            var products = await _repository.Cart.GetCartProducts(user);

            var productCart = products.Any(p => p.Id == product.Id);
            if (productCart)
            {
                _logger.LogError($"Product with id: {productId} exist in cart");
                return BadRequest($"Product with id: {productId} exist in cart");
            }

            _repository.Cart.CreateCartProduct(cart);

            await _repository.SaveAsync();

            var cartDto = _mapper.Map<CartOutgoingDto>(cart);

            return Ok(cartDto);
        }


        /// <summary> Delete user after authenticate </summary>
        /// <param name="productId"></param>
        /// <returns>No content</returns>
        [HttpDelete(Name = "DeleteCartProduct")]
        [Authorize]
        [ServiceFilter(typeof(ValidateCartAttribute))]
        public async Task<IActionResult> DeleteCartProduct(int productId)
        {
            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = new Cart { UserId = user.Id, ProductId = product.Id };

            _repository.Cart.DeleteCartProduct(cart);

            return NoContent();
        }
    }
}
