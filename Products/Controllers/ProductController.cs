﻿using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        private readonly IDataShaper<ProductDto> _dataShaper;
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             IDataShaper<ProductDto> dataShaper, ICurrencyApiConnection currencyConnection)
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

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(products);

            return Ok(_dataShaper.ShapeData(productsDto, productParameters.Fields));
        }

        /// <summary> Get product by id </summary>
        /// <param name="id"></param>
        /// <param name="productParameters"></param>
        /// <returns> Product with a given id</returns>
        [HttpGet("{id}", Name = "GetProduct")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public IActionResult GetProduct(int id, [FromQuery] ProductParameters productParameters)
        {
            if (productParameters.MaxCost < productParameters.MinCost)
                return BadRequest("Invalid cost range");

            var productEntity = HttpContext.Items["product"] as Product;

            var productDto = _mapper.Map<ProductDto>(productEntity);

            return Ok(_dataShaper.ShapeData(productDto, productParameters.Fields));
        }

        /// <summary> Create newly product </summary>
        /// <param name="product"></param>
        /// <returns> Created product with id </returns>
        [HttpPost(Name = "CreateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductManipulationAttribute))]
        public async Task<IActionResult> CreateProduct(
            [FromBody] ProductForManipulationDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return RedirectToRoute("GetProduct",
                new { id = productToReturn.Id });
        }

        /// <summary> Update an existing product by id </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns> No content </returns>
        [HttpPut("{id}", Name = "UpdateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        [ServiceFilter(typeof(ValidateProductManipulationAttribute))]
        public async Task<IActionResult> UpdateProduct(int id,
            [FromBody] ProductForManipulationDto product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary> Partical update an existing product by id </summary>
        /// <param name="id"></param>
        /// <param name="patchDoc"></param>
        /// <returns> No content </returns>
        [HttpPatch("{id}", Name = "PartiallyUpdateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateProduct(int id,
            [FromBody] JsonPatchDocument<ProductForManipulationDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var productEntity = HttpContext.Items["product"] as Product;

            var productToPatch = _mapper.Map<ProductForManipulationDto>(productEntity);

            patchDoc.ApplyTo(productToPatch, ModelState);

            if (ModelState.IsValid == false)
            {
                _logger.LogError("Invalid model state for the patch document");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(productToPatch, productEntity);

            await _repository.SaveAsync();

            return NoContent();
        }

        /// <summary> Delete an existing product by id </summary>
        /// <param name="id"></param>
        /// <returns> No content </returns>
        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _repository.Product.DeleteProduct(productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
