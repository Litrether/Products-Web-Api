using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProviderAttribute : ValidationFilterAttribute<Provider>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateProviderAttribute(IRepositoryManager repository,
            ILoggerManager logger)
            : base(logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (IsValidRequstModel(context) == false)
                return;

            if (context.HttpContext.Request.Method != "POST")
            {
                var id = (int)context.ActionArguments["id"];

                var provider = await _repository.Provider.GetProviderAsync(id, trackChanges: false);
                if (IsNullEntity(context, provider, id))
                    return;
            }
            await next();
        }
    }
}
