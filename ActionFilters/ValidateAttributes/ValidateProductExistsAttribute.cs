using System.Threading.Tasks;
using System.Web.Mvc;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Products.ActionFilters
{
    public class ValidateProductExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProductExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        //todo доделать валидацию продуктов
        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            var trackChanges =
                (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var id = (int)context.ActionArguments["id"];
            var product = await _repository.Product.GetProductAsync(id, trackChanges);

            var categoryId = product.CategoryId;
            var providerId = product.ProviderId;

            var category = await _repository.Category.GetCategoryAsync(categoryId, false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {categoryId} doesn't exist in the database.");

                context.Result = new NotFoundResult();
                return;
            }

            var provider = await _repository.Provider.GetProviderAsync(providerId, false);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {providerId} doesn't exist in the database.");

                context.Result = new NotFoundResult();
                return;
            }

            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            product.Provider = provider;
            product.Category = category;

            context.HttpContext.Items.Add("product", product);
            await next();
        }
    }
}
