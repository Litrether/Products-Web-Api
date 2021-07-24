using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public class ValidateAccountAttribute : ValidationFilterAttribute<User>, IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IAutenticationManager _authManager;

        public ValidateAccountAttribute(ILoggerManager logger, IAutenticationManager authManager)
            : base(logger)
        {
            _logger = logger;
            _authManager = authManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var requestPath = context.HttpContext.Request.Path.Value;
            var needValidateUser = requestPath.Contains("login") || requestPath.Contains("delete");
            var method = context.HttpContext.Request.Method;

            if (IsValidRequstModel(context, method) == false)
                return;

            if (needValidateUser && await IsValidUser(context))
                return;

            await next();
        }

        private async Task<bool> IsValidUser(ActionExecutingContext context)
        {
            var user = context.ActionArguments["user"] as UserValidationDto;

            if (await _authManager.ValidateUser(user))
            {
                _logger.LogWarn($"Authentication failed. Wrong username or password.");
                context.Result = new UnauthorizedResult();
                return false;
            }

            return true;
        }
    }
}
