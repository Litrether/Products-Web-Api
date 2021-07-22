using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

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

            if (IsValidRequstModel(context) == false)
                return;

            if (needValidateUser && await TryValidateUser(context))
                return;

            await next();
        }

        private async Task<bool> TryValidateUser(ActionExecutingContext context)
        {
            var user = context.ActionArguments["user"] as UserForManipulationDto;

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
