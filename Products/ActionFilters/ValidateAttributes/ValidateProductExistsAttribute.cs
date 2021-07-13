using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            var trackChanges =
                (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;

            var id = (int)context.ActionArguments["id"];

            var product = await _repository.Product.GetProductAsync(id, trackChanges);
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
                return;
            }

            context.HttpContext.Items.Add("product", product);

            await next();
        }
    }
}
