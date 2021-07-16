using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Products.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [ResponseCache(CacheProfileName = "180SecondsDuration")]
    [ApiExplorerSettings(GroupName = "v1")]
    public class CategoryController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCategories")]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> GetCategories(
            [FromQuery] CategoryParameters categoryParameters)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(
                categoryParameters, trackChanges: false);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}", Name = "GetCategory")]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public IActionResult GetCategory(int id)
        {
            var categoryEntity = HttpContext.Items["category"] as Category;

            var categoryDto = _mapper.Map<CategoryDto>(categoryEntity);
            return Ok(categoryDto);
        }

        [HttpPost(Name = "CreateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CategoryForManipulationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity);
            await _repository.SaveAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute("GetCategory",
                new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [HttpPut("{id}", Name = "UpdateCategory")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> UpdateCategory(int id,
            [FromBody] CategoryForManipulationDto category)
        {
            var categoryEntity = HttpContext.Items["category"] as Category;

            _mapper.Map(category, categoryEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpDelete("{id}", Name = "DeleteCategory")]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryEntity = HttpContext.Items["category"] as Category;

            _repository.Category.DeleteCategory(categoryEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}
