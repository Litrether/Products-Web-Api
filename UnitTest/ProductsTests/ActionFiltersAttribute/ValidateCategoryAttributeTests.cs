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
    public class ValidateCategoryAttributeTests
    {
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        private Mock<ActionExecutionDelegate> _next;
        private ActionExecutingContext _actionExecutingContext;
        private ValidateCategoryAttribute _filter;

        public ValidateCategoryAttributeTests()
        {
            _next = new Mock<ActionExecutionDelegate>();
            _filter = new ValidateCategoryAttribute(_repo.Object, _logger.Object);
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
        public async void PostValidateCategoryAttributeInvokeNext()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "category", new CategoryIncomingDto() }
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PostValidateCategoryAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PostValidateCategoryAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "category", new CategoryIncomingDto() }
            };
            SetActionExecutingContext("POST", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateCategoryAttributeInvokeNext()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "category", new CategoryIncomingDto() }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PutValidateCategoryAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateCategoryAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "category", new CategoryIncomingDto() }
            };
            SetActionExecutingContext("PUT", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateCategoryAttributeReturnsNotFoundResultWhenCategoryNotFound()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "category", new CategoryIncomingDto() }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateCategoryAttributeInvokeNext()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void GetValidateCategoryAttributeReturnsNotFoundResultWhenCategoryNotFound()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateCategoryAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateCategoryAttributeInvokeNext()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void DeleteValidateCategoryAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateCategoryAttributeReturnsNotFoundResultWhenCategoryNotFound()
        {
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }
    }
}
