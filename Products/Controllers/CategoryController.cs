using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.DataTransferObjects.Outcoming;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Products.ActionFilters;

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

        /// <summary> Get list of all categories </summary>
        /// <param name="categoryParameters"></param>
        /// <returns>The categories list</returns>
        [HttpGet(Name = "GetCategories")]
        [Authorize]
        [ResponseCache(Duration = 120)]
        public async Task<IActionResult> GetCategories(
            [FromQuery] CategoryParameters categoryParameters)
        {
            var categories = await _repository.Category.GetAllCategoriesAsync(
                categoryParameters, trackChanges: false);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryOutgoingDto>>(categories);

            return Ok(new { pagination = categories.MetaData, categories = categoriesDto });
        }

        /// <summary> Get category by id </summary>
        /// <param name="id"></param>
        /// <returns>Category with a given id</returns>
        [HttpGet("{id}", Name = "GetCategory")]
        [Authorize]
        [ServiceFilter(typeof(ValidateCategoryAttribute))]
        public async Task<IActionResult> GetCategory(int id)
        {
            var categoryEntity = await _repository.Category.GetCategoryAsync(id, trackChanges: false);

            var categoryDto = _mapper.Map<CategoryOutgoingDto>(categoryEntity);

            return Ok(categoryDto);
        }

        /// <summary> Create newly category </summary>
        /// <param name="category"></param>
        /// <returns> Created category with id </returns>
        [HttpPost(Name = "CreateCategory")]
        [Authorize(Roles = ("Administrator"))]
        [ServiceFilter(typeof(ValidateCategoryAttribute))]
        public async Task<IActionResult> CreateCategory(
            [FromBody] CategoryIncomingDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity);

            try
            {
                await _repository.SaveAsync();
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.InnerException.Message);
            }

            var categoryToReturn = _mapper.Map<CategoryOutgoingDto>(categoryEntity);

            return RedirectToRoute("GetCategory",
                new { id = categoryToReturn.Id });
        }

        /// <summary> Update an existing category by id </summary>
        /// <param name="id"></param>
        /// <param name="category"></param>
        /// <returns> No content </returns>
        [HttpPut("{id}", Name = "UpdateCategory")]
        [ServiceFilter(typeof(ValidateCategoryAttribute))]
        [Authorize(Roles = ("Administrator"))]
        public async Task<IActionResult> UpdateCategory(int id,
            [FromBody] CategoryIncomingDto category)
        {
            var categoryEntity = await _repository.Category.GetCategoryAsync(id, trackChanges: true);

            _mapper.Map(category, categoryEntity);

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

        /// <summary> Delete an existing category by id </summary>
        /// <param name="id"></param>
        /// <returns> No content </returns>
        [HttpDelete("{id}", Name = "DeleteCategory")]
        [Authorize(Roles = ("Administrator"))]
        [ServiceFilter(typeof(ValidateCategoryAttribute))]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var categoryEntity = await _repository.Category.GetCategoryAsync(id, trackChanges: false);

            _repository.Category.DeleteCategory(categoryEntity);

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
