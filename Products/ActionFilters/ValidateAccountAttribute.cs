using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace Products.ActionFilters
{
    public class ValidateAccountAttribute : ValidationFilterAttribute<User>, IAsyncActionFilter
    {
        private readonly ILoggerManager _logger;
        private readonly IAutenticationManager _authManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public ValidateAccountAttribute(ILoggerManager logger,
            IAutenticationManager authManager, RoleManager<IdentityRole> roleManager)
            : base(logger)
        {
            _logger = logger;
            _authManager = authManager;
            _roleManager = roleManager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var requestPath = context.HttpContext.Request.Path.Value;
            var needValidateUser = requestPath.EndsWith("/login");
            var needValidateRoles = requestPath.EndsWith("/account");
            var method = context.HttpContext.Request.Method;

            if (IsValidRequstModel(context, method) == false)
                return;

            if (needValidateUser && await IsValidUser(context) == false)
                return;

            if (needValidateRoles && await IsValidRoles(context) == false)
                return;

            await next();
        }

        private async Task<bool> IsValidUser(ActionExecutingContext context)
        {
            var user = context.ActionArguments["user"] as UserValidationDto;

            if (await _authManager.ValidateUser(user) == false)
            {
                _logger.LogWarn($"Authentication failed. Wrong username or password.");
                context.Result = new UnauthorizedResult();
                return false;
            }

            return true;
        }

        private async Task<bool> IsValidRoles(ActionExecutingContext context)
        {
            var userRoles = (context.ActionArguments["userForRegistration"] as UserRegistrationDto).Roles.ToList();
            var existRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            var notExistUserRoles = userRoles.Where(r => existRoles.Contains(r) == false).ToList();

            if (notExistUserRoles.Count > 0)
            {
                context.Result = new BadRequestObjectResult($"One or all roles doesn't exist in database: {string.Join(", ", notExistUserRoles)} \n\r" +
                    $"Possible roles: Administrator, Manager and User");

                return false;
            }

            return true;
        }
    }
}
