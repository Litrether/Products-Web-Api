using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

namespace Products.Controllers
{
    //todo Add for methods and controller role access
    [Route("api/products")]
    [ApiController]
    //[Authorize]
    [ResponseCache(CacheProfileName = "180SecondsDuration")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class ProductController : ControllerBase
    {
        private readonly ICurrencyApiConnection _currencyConnection;
        private readonly IDataShaper<ProductOutgoingDto> _dataShaper;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             IDataShaper<ProductOutgoingDto> dataShaper, ICurrencyApiConnection currencyConnection)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _currencyConnection = currencyConnection;
        }

        /// <summary> Get list of all products </summary>
        /// <param name="productParameters"></param>
        /// <returns>The products list</returns>
        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts(
            [FromQuery] ProductParameters productParameters)
        {
            if (productParameters.MaxCost < productParameters.MinCost)
                return BadRequest("Invalid cost range.");

            var exchangeRate = _currencyConnection.GetExchangeRate(productParameters.Currency);

            var products = await _repository.Product.GetAllProductsAsync(productParameters, trackChanges: false, exchangeRate);

            var productsDto = _mapper.Map<IEnumerable<ProductOutgoingDto>>(products);

            return Ok(_dataShaper.ShapeData(productsDto, productParameters.Fields));
        }

        /// <summary> Get product by id </summary>
        /// <param name="id"></param>
        /// <param name="productParameters"></param>
        /// <returns> Product with a given id</returns>
        [HttpGet("{id}", Name = "GetProduct")]
        [ServiceFilter(typeof(ValidateProductAttribute))]
        public async Task<IActionResult> GetProduct(int id, [FromQuery] ProductParameters productParameters)
        {
            if (productParameters.MaxCost < productParameters.MinCost)
                return BadRequest("Invalid cost range.");

            var exchangeRate = _currencyConnection.GetExchangeRate(productParameters.Currency);

            var productEntity = await _repository.Product.GetProductAsync(id, trackChanges: false, exchangeRate);

            var productDto = _mapper.Map<ProductOutgoingDto>(productEntity);

            return Ok(_dataShaper.ShapeData(productDto, productParameters.Fields));
        }

        /// <summary> Create newly product </summary>
        /// <param name="product"></param>
        /// <returns> Created product with id </returns>
        [HttpPost(Name = "CreateProduct")]
        [ServiceFilter(typeof(ValidateProductAttribute))]
        public async Task<IActionResult> CreateProduct(
            [FromBody] ProductIncomingDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            var productToReturn = _mapper.Map<ProductOutgoingDto>(productEntity);

            return RedirectToRoute("GetProduct",
                new { id = productToReturn.Id });
        }

        /// <summary> Update an existing product by id </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns> No content </returns>
        [HttpPut("{id}", Name = "UpdateProduct")]
        [ServiceFilter(typeof(ValidateProductAttribute))]
        public async Task<IActionResult> UpdateProduct(int id,
            [FromBody] ProductIncomingDto product)
        {
            var productEntity = await _repository.Product.GetProductAsync(id, trackChanges: true);

            _mapper.Map(product, productEntity);

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

        /// <summary> Partical update an existing product by id </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns> No content </returns>
        [HttpPatch("{id}", Name = "PartiallyUpdateProduct")]
        [ServiceFilter(typeof(ValidateProductAttribute))]
        public async Task<IActionResult> PartiallyUpdateProduct(int id,
            [FromBody] JsonPatchDocument<ProductIncomingDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("PatchDoc object is null.");
            }

            var productEntity = await _repository.Product.GetProductAsync(id, trackChanges: true);

            var productToPatch = _mapper.Map<ProductIncomingDto>(productEntity);

            patchDoc.ApplyTo(productToPatch, ModelState);

            if (ModelState.IsValid == false)
            {
                _logger.LogError("Invalid model state for the patch document.");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(productToPatch, productEntity);

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

        /// <summary> Delete an existing product by id </summary>
        /// <param name="id"></param>
        /// <returns> No content </returns>
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ServiceFilter(typeof(ValidateProductAttribute))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productEntity = await _repository.Product.GetProductAsync(id, trackChanges: false);

            _repository.Product.DeleteProduct(productEntity);

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
