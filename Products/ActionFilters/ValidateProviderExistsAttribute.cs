using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProviderExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProviderExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var trackChanges = context.HttpContext.Request.Method.Equals("PUT");
            var id = (int)context.ActionArguments["id"];

            var provider = await _repository.Provider.GetProviderAsync(id, trackChanges);
            if (provider == null)
            {
                _logger.LogInfo($"Provider with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return;
            }

            context.HttpContext.Items.Add("provider", provider);
            await next();
        }
    }
}
