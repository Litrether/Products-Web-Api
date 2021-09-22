using Contracts;
using Entities.DataTransferObjects.Incoming;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Products.ActionFilters;
using System.Collections.Generic;
using Xunit;

namespace UnitTests.ProductsTests.ActionFiltersAttribute
{
    public class ValidateProviderAttributeTests
    {
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        private Mock<ActionExecutionDelegate> _next;
        private ActionExecutingContext _actionExecutingContext;
        private ValidateProviderAttribute _filter;

        public ValidateProviderAttributeTests()
        {
            _next = new Mock<ActionExecutionDelegate>();
            _filter = new ValidateProviderAttribute(_repo.Object, _logger.Object);
        }

        private void SetActionExecutingContext(string method, bool modelInvalid, Dictionary<string, object> args)
        {
            var modelState = new ModelStateDictionary();
            if (modelInvalid)
                modelState.AddModelError("name", "test invalid");
            var httpContext = new Mock<HttpContext>();
            httpContext.Setup(x => x.Request.Method).Returns(method);
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
        public async void PostValidateProviderAttributeInvokeNext()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "provider", new ProviderIncomingDto() }
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PostValidateProviderAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PostValidateProviderAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "provider", new ProviderIncomingDto() }
            };
            SetActionExecutingContext("POST", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProviderAttributeInvokeNext()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "provider", new ProviderIncomingDto() }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PutValidateProviderAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProviderAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "provider", new ProviderIncomingDto() }
            };
            SetActionExecutingContext("PUT", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProviderAttributeReturnsNotFoundResultWhenProviderNotFound()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "provider", new ProviderIncomingDto() }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateProviderAttributeInvokeNext()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void GetValidateProviderAttributeReturnsNotFoundResultWhenProviderNotFound()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateProviderAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateProviderAttributeInvokeNext()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void DeleteValidateProviderAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateProviderAttributeReturnsNotFoundResultWhenProviderNotFound()
        {
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }
    }
}
