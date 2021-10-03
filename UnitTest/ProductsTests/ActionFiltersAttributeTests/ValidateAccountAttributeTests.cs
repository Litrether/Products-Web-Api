using Contracts;
using Entities.DataTransferObjects.Incoming;
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
namespace UnitTests.ProductsTests.ActionFiltersAttributeTests
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
            var store = new Mock<IRoleStore<IdentityRole>>();
            _roleManager = new Mock<RoleManager<IdentityRole>>(store.Object, null, null, null, null);
            _roleManager.Setup(x => x.Roles).Returns(
                new List<IdentityRole>() {
                    new IdentityRole(){Name = "Administrator"},
                    new IdentityRole(){Name = "Manager"},
                    new IdentityRole(){Name = "User"},
                }.AsQueryable);
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
            httpContext.Setup(x => x.Request.Path).Returns(path);
            var routeData = new RouteData();
            routeData.Values.Add("controller", "Account");
            var actionDescriptor = new Mock<ActionDescriptor>();

            var actionContext = new ActionContext(httpContext.Object, routeData, actionDescriptor.Object, modelState);
            _actionExecutingContext = new ActionExecutingContext(
                    actionContext,
                    new List<IFilterMetadata>(),
                    args,
                    Mock.Of<Controller>()
                );
        }

        [Fact]
        public async void RegisterValidateAccountAttributeInvokeNext()
        {
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "userForRegistration" , new UserRegistrationDto(){ Roles = new List<string>() { "User" } } },
            };
            SetActionExecutingContext("POST", "/account", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void RegisterValidateAccountAttributeReturnsBadRequestResultObjectWhenRoleInvalid()
        {
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "userForRegistration" , new UserRegistrationDto(){ Roles = new List<string>() { "InvalidRole" } } },
            };
            SetActionExecutingContext("POST", "/account", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void RegisterValidateAccountAttributeReturnsUnprocessableEntityObjectResultWhenModelInvalid()
        {
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "userForRegistration" , new UserRegistrationDto(){ Roles = new List<string>() { "InvalidRole" } } },
            };
            SetActionExecutingContext("POST", "/account", true, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void RegisterValidateAccountAttributeReturnsBadRequestResultObjectIsNull()
        {
            var args = new Dictionary<string, object>() {
                { "id", 1 },
            };
            SetActionExecutingContext("POST", "/account", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void AuthenticateValidateAccountAttributeInvokeNext()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(true);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("POST", "/login", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void AuthenticateValidateAccountAttributeReturnsUnauthorizedResultWhenUserInvalid()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("POST", "/login", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnauthorizedResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void AuthenticateValidateAccountAttributeReturnsBadRequestObjectResultWhenObjectIsNull()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
            };
            SetActionExecutingContext("POST", "/login", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void AuthenticateValidateAccountAttributeReturnsUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("POST", "/login", true, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateAccountAttributeInvokeNext()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(true);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("DELETE", "", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void DeleteValidateAccountAttributeReturnsUnauthorizedResultWhenUserInvalid()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("DELETE", "", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnauthorizedResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateAccountAttributeReturnsBadRequestObjectResultWhenObjectIsNull()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
            };
            SetActionExecutingContext("DELETE", "", false, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateAccountAttributeReturnsUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _authManager.Setup(x => x.ValidateUser(It.IsAny<UserValidationDto>())).ReturnsAsync(false);
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "user" , new UserValidationDto() },
            };
            SetActionExecutingContext("DELETE", "", true, args);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }
    }
}
