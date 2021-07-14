﻿using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public class ValidationFilterAttribute : IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;

        public ValidationFilterAttribute(ILoggerManager logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

            var param = context.ActionArguments
                .SingleOrDefault(x => x.Value.ToString().Contains("Dto")).Value;
            if (param == null)
            {
                _logger.LogError($"Object sent from client is null. Controller: {controller}, action: { action}");
                context.Result = new BadRequestObjectResult($"Object is null. Controller: { controller }, action: { action} ");
                return;
            }

            if (context.ModelState.IsValid == false)
            {
                _logger.LogError($"Invalid model state for the object. Controller: { controller}, action: { action} ");
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }

            await next();
        }
    }
}