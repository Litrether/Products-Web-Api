using System.Linq;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public abstract class ValidationFilterAttribute<T> where T : class
    {
        private readonly ILoggerManager _logger;

        public ValidationFilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public bool IsValidRequstModel(ActionExecutingContext context, string method)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var methodHasDtoParam = method == "PATCH" ||
                                    method == "POST" ||
                                    method == "PUT";

            if (methodHasDtoParam)
            {
                var param = context.ActionArguments.SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
                if (param == null)
                {
                    _logger.LogError($"Object sent from client is null. Controller: { controller }, action: { action }.");
                    context.Result = new BadRequestObjectResult($"Object is null. Controller: { controller }, action: { action }.");
                    return false;
                }
            }

            if (context.ModelState.IsValid == false)
            {
                _logger.LogError($"Invalid model state for the object. Controller: { controller }, action: { action }.");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
                return false;
            }

            return true;
        }



        public bool IsNullEntity(ActionExecutingContext context, T entity, int id)
        {
            if (entity == null)
            {
                _logger.LogInfo($"Provider with id: {id} doesn't exist in the database.");
                context.Result = new NotFoundResult();
                return true;
            }

            return false;
        }
    }
}
