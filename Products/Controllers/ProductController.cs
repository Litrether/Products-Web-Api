using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

namespace Products.Controllers
{
    //todo add for methods and controller role access
    [ApiVersion("1.0")]
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly IDataShaper<ProductDto> _dataShaper;
        private readonly ICurrencyConverterManager _currencyConverter;

        public ProductController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper,
             IDataShaper<ProductDto> dataShaper, ICurrencyConverterManager currencyConverter)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _dataShaper = dataShaper;
            _currencyConverter = currencyConverter;
        }

        [HttpGet(Name = "GetProducts")]
        public async Task<IActionResult> GetProducts(
            [FromQuery] ProductParameters productParameters)
        {
            var products = await _repository.Product.GetAllProductsAsync(
                productParameters, trackChanges: false);

            var changedProduct = _currencyConverter.ChangeCurrency(products, productParameters.Currency);

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(changedProduct);

            return Ok(_dataShaper.ShapeData(productsDto, productParameters.Fields));
        }

        [HttpGet("{id}", Name = "GetProduct")]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public IActionResult GetProduct(int id, [FromQuery] ProductParameters productParameters)
        {
            var productEntity = HttpContext.Items["product"] as Product;
            
            var changedProduct = _currencyConverter.ChangeCurrency(productEntity, productParameters.Currency);

            var productDto = _mapper.Map<ProductDto>(changedProduct);

            return Ok(_dataShaper.ShapeData(productDto, productParameters.Fields));
        }

        [HttpPost(Name = "CreateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductManipulationAttribute))]
        public async Task<IActionResult> CreateProduct(
            [FromBody] ProductForManipilationDto product)
        {
            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProduct(productEntity);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            //todo Repair rerouting to GetProduct
            return CreatedAtRoute("GetProduct",
                new { id = productToReturn.Id }, productToReturn);
        }

        [HttpPut("{id}", Name = "UpdateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        [ServiceFilter(typeof(ValidateProductManipulationAttribute))]
        public async Task<IActionResult> UpdateProduct(int id,
            [FromBody] ProductForManipilationDto Product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(Product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}", Name = "PartiallyUpdateProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateProduct(int id,
            [FromBody] JsonPatchDocument<ProductForManipilationDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var productEntity = HttpContext.Items["product"] as Product;

            var productToPatch = _mapper.Map<ProductForManipilationDto>(productEntity);

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

        [HttpDelete("{id}", Name = "DeleteProduct")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
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
