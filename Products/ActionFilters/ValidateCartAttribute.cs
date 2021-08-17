using System.Threading.Tasks;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public class ValidateCartAttribute : ValidationFilterAttribute<Cart>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateCartAttribute(IRepositoryManager repository,
            ILoggerManager logger)
            : base(logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var productId = (int)context.ActionArguments["productId"];

            var product = await _repository.Product.GetProductAsync(productId, trackChanges: false);
            if (product == null)
            {
                _logger.LogError($"Product with id: {productId} doesn't exist in database.");
                context.Result = new BadRequestObjectResult($"Product with id: {productId} doesn't exist in database.");
                return;
            }

            await next();
        }
    }
}
