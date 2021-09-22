using AutoMapper;
using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Products.ActionFilters;
using System.Collections.Generic;
using System.Linq;
using Xunit;
namespace UnitTests.ProductsTests.ActionFiltersAttribute
{
    public class ValidateAccountAttributeTests
    {
        Mock<IAutenticationManager> _authManager = new Mock<IAutenticationManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        Mock<RoleManager<IdentityRole>> _roleManager;
        private Mock<ActionExecutionDelegate> _next;
        private ActionExecutingContext _actionExecutingContext;
        private ValidateAccountAttribute _filter;

        public ValidateAccountAttributeTests()
        {
            _roleManager = new Mock<RoleManager<IdentityRole>>();
            _roleManager.Setup(x => x.Roles).Returns(
                new List<IdentityRole>() {
                    new IdentityRole(){Name = "Administrator"},
                    new IdentityRole(){Name = "Manager"},
                    new IdentityRole(){Name = "User"},
            }.AsQueryable());
            _next = new Mock<ActionExecutionDelegate>();
            _filter = new ValidateAccountAttribute(_logger.Object, _authManager.Object, _roleManager.Object);
        }

        private void SetActionExecutingContext(string method, string path, bool modelInvalid, Dictionary<string, object> args)
        {
            var modelState = new ModelStateDictionary();
            if (modelInvalid)
                modelState.AddModelError("name", "test invalid");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request.Method).Returns(method);
            httpContext.Setup(x => x.Request.Path.Value).Returns(path);
            var routeData = new Mock<RouteData>();
            var actionDescriptor = new Mock<ActionDescriptor>();

            var actionContext = new ActionContext(httpContext.Object, routeData.Object, actionDescriptor.Object, modelState);
            _actionExecutingContext = new ActionExecutingContext(
                    actionContext,
                    new List<IFilterMetadata>(),
                    args,
                    Mock.Of<Controller>()
                );
        }

        [Fact]
        public async void RegisterValidateCategoryAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserRegistrationDto(){ Roles = new List<string>() { "User" } } },
            };
            SetActionExecutingContext("POST", "/account", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

    }
}
