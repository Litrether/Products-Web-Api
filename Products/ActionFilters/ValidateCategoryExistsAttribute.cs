using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateCategoryExistsAttribute : ValidationFilterAttribute<Category>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateCategoryExistsAttribute(IRepositoryManager repository, 
            ILoggerManager logger)
            : base (logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            switch (method)
            {
                case "GET":
                    await GetByIdFilter(context, next);
                    break;
                case "POST":
                    await PostFilter(context, next);
                    break;
                case "PUT":
                    await PutFilter(context, next);
                    break;
                case "DELETE":
                    await DeleteFilter(context, next);
                    break;
            }
        }

        private async Task GetByIdFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];

            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
            if (IsNullEntity(context, category, id))
                return;

            await next();
        }

        private async Task PostFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (IsValidRequstModel(context) == false)
                return;

            var categoryForCreate = context.ActionArguments["category"] as CategoryForManipulationDto;

            var isExistInDatabase = await _repository.Category.CheckExistByName(categoryForCreate.Name, trackChanges: false);
            if (isExistInDatabase)
            {
                _logger.LogInfo($"Category with name: \"{categoryForCreate.Name}\" exists in the database");
                context.Result = new BadRequestObjectResult($"Category with name: \"{categoryForCreate.Name}\" exists in the database");
                return;
            }

            await next();
        }

        private async Task PutFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (IsValidRequstModel(context) == false)
                return;

            var id = (int)context.ActionArguments["id"];
            var categoryForUpdate = context.ActionArguments["category"] as CategoryForManipulationDto;

            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
            if (IsNullEntity(context, category, id))
                return;

            var isExistInDatabase = await _repository.Category.CheckExistByName(categoryForUpdate.Name, trackChanges: false);
            if (isExistInDatabase)
            {
                _logger.LogInfo($"Category with name: \"{categoryForUpdate.Name}\" exists in the database");
                context.Result = new BadRequestObjectResult($"Category with name: \"{categoryForUpdate.Name}\" exists in the database");
                return;
            }

            await next();
        }

        private async Task DeleteFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];

            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
            if (IsNullEntity(context, category, id))
                return;

            await next();
        }

    }
}
