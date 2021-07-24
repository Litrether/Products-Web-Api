using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public class ValidateProductAttribute : ValidationFilterAttribute<Product>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProductAttribute(IRepositoryManager repository,
            ILoggerManager logger)
            : base(logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var needCheckRelatedElements = method == "POST" || method == "PUT";

            if (IsValidRequstModel(context, method) == false)
                return;

            if (method != "POST")
            {
                var id = (int)context.ActionArguments["id"];

                var product = await _repository.Product.GetProductAsync(id, trackChanges: false);
                if (IsNullEntity(context, product, id))
                    return;
            }

            if (needCheckRelatedElements && await RelatedElementsExist(context) == false)
                return;

            await next();
        }

        private async Task<bool> RelatedElementsExist(ActionExecutingContext context)
        {
            var productForCreate = context.ActionArguments["product"] as ProductIncomingDto;

            var category = await _repository.Category.GetCategoryAsync(productForCreate.CategoryId, false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {productForCreate.CategoryId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Category with id: {productForCreate.CategoryId} doesn't exist in the database.");
                return false;
            }

            var provider = await _repository.Provider.GetProviderAsync(productForCreate.ProviderId, false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {productForCreate.ProviderId} doesn't exist in the database.");

                context.Result = new BadRequestObjectResult($"Provider with id: {productForCreate.ProviderId} doesn't exist in the database.");
                return false;
            }

            return true;
        }

        //todo Create Patch validation filter
        private async Task PatchFilter(ActionExecutingContext context,
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
