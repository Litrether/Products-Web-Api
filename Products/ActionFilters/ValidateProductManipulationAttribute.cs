using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProductManipulationAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProductManipulationAttribute(IRepositoryManager repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var product = context.ActionArguments["product"] as ProductForManipulationDto;

            var category = await _repository.Category.GetCategoryAsync(product.CategoryId, false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {product.CategoryId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Category with id: {product.CategoryId} doesn't exist in the database.");
                return;
            }

            var provider = await _repository.Provider.GetProviderAsync(product.ProviderId, false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {product.ProviderId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Provider with id: {product.ProviderId} doesn't exist in the database.");
                return;
            }

            await next();
        }
    }
}
