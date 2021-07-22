using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProductExistsAttribute : ValidationFilterAttribute<Product>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProductExistsAttribute(IRepositoryManager repository,
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
                case "PATCH":
                    await PatchFilter(context, next);
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

            var product = await _repository.Product.GetProductAsync(id, trackChanges: false);
            if (IsNullEntity(context, product, id))
                return;

            await next();
        }

        private async Task PostFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (IsValidRequstModel(context) == false)
                return;

            var productForCreate = context.ActionArguments["product"] as ProductForManipulationDto;

            var category = await _repository.Category.GetCategoryAsync(productForCreate.CategoryId, false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {productForCreate.CategoryId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Category with id: {productForCreate.CategoryId} doesn't exist in the database.");
                return;
            }

            var provider = await _repository.Provider.GetProviderAsync(productForCreate.ProviderId, false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {productForCreate.ProviderId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Provider with id: {productForCreate.ProviderId} doesn't exist in the database.");
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

            var product = await _repository.Product.GetProductAsync(id, trackChanges: false);
            if (IsNullEntity(context, product, id))
                return;

            var productForUpdate = context.ActionArguments["product"] as ProductForManipulationDto;

            var category = await _repository.Category.GetCategoryAsync(productForUpdate.CategoryId, false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {productForUpdate.CategoryId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Category with id: {productForUpdate.CategoryId} doesn't exist in the database.");
                return;
            }

            var provider = await _repository.Provider.GetProviderAsync(productForUpdate.ProviderId, false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {productForUpdate.ProviderId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Provider with id: {productForUpdate.ProviderId} doesn't exist in the database.");
                return;
            }

            await next();
        }

        private async Task PatchFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (IsValidRequstModel(context) == false)
                return;

            var id = (int)context.ActionArguments["id"];

            var product = await _repository.Product.GetProductAsync(id, trackChanges: false);
            if (IsNullEntity(context, product, id))
                return;

            await next();
        }

        private async Task DeleteFilter(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var id = (int)context.ActionArguments["id"];

            var product = await _repository.Product.GetProductAsync(id, trackChanges: false);
            if (IsNullEntity(context, product, id))
                return;

            await next();
        }
    }
}
