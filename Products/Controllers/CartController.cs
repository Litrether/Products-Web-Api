using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICurrencyApiConnection _currencyConnection;
        private readonly IRepositoryManager _repository;
        private readonly UserManager<User> _userManager;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CartController(ICurrencyApiConnection currencyConnection,
            IRepositoryManager repository, ILoggerManager logger,
            UserManager<User> userManager, IMapper mapper)
        {
            _currencyConnection = currencyConnection;
            _repository = repository;
            _userManager = userManager;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary> Get products from a user basket </summary>
        /// <returns> Products from a user basket</returns>
        [HttpGet(Name = "GetCartProducts")]
        [Authorize]
        public async Task<IActionResult> GetCartProducts(
            [FromQuery] ProductParameters productParameters)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var exchangeRate = _currencyConnection.GetExchangeRate(productParameters.Currency);

            var products = await _repository.Cart.GetCartProductsAsync(productParameters,
                user, trackChanges: false, exchangeRate);
            Response?.Headers?.Add("pagination", JsonSerializer.Serialize(products.MetaData));

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
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = await _repository.Cart.GetCartProductAsync(user, productId,  trackChanges: false);
            if (cart != null)
            {
                _logger.LogError($"Product with id: {productId} is in cart");
                return BadRequest($"Product with id: {productId} is in cart");
            }
            var newCart = new Cart { UserId = user.Id, ProductId = productId };

            _repository.Cart.CreateCartProduct(newCart);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);
            var productDto = _mapper.Map<ProductOutgoingDto>(product);

            return Ok(productDto);
        }


        /// <summary> Delete product from user cart</summary>
        /// <param name="productId"></param>
        /// <returns>No content</returns>
        [HttpDelete("{productId}", Name = "DeleteCartProduct")]
        [Authorize]
        [ServiceFilter(typeof(ValidateCartAttribute))]
        public async Task<IActionResult> DeleteCartProduct(int productId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cart = await _repository.Cart.GetCartProductAsync(user, productId, trackChanges: false);
            if (cart == null)
            {
                _logger.LogError($"Cart with id: {productId} not found.");
                return NotFound();
            }

            _repository.Cart.DeleteCartProduct(cart);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            return NoContent();
        }
    }
}
