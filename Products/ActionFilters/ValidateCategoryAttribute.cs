using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateCategoryAttribute : ValidationFilterAttribute<Category>, IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;

        public ValidateCategoryAttribute(IRepositoryManager repository,
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

            if(context.HttpContext.Request.Method != "POST")
            {
                var id = (int)context.ActionArguments["id"];

                var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);
                if (IsNullEntity(context, category, id))
                    return;
            }


            await next();
        }

    }
}
