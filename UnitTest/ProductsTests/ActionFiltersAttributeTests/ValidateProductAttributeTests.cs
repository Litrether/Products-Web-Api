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

namespace UnitTests.ProductsTests.ActionFiltersAttributeTests
{
    public class ValidateProductAttributeTests
    {
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        private Mock<ActionExecutionDelegate> _next;
        private ActionExecutingContext _actionExecutingContext;
        private ValidateProductAttribute _filter;

        public ValidateProductAttributeTests()
        {
            _next = new Mock<ActionExecutionDelegate>();
            _filter = new ValidateProductAttribute(_repo.Object, _logger.Object);
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
        public async void PostValidateProductAttributeInvokeNext()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PostValidateProductAttributeReturnBadRequestObjectResultWhenCategoryNotExist()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PostValidateProductAttributeReturnBadRequestObjectResultWhenProviderNotExist()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PostValidateProductAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("POST", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PostValidateProductAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "product", new ProductIncomingDto() }
            };
            SetActionExecutingContext("POST", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProductAttributeInvokeNext()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void PutValidateProductAttributeReturnBadRequestObjectResultWhenObjectisNull()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() {
                { "id", 1 }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProductAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() {
                { "id", 1 },
                { "product", new ProductIncomingDto() }
            };
            SetActionExecutingContext("PUT", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProductAttributeReturnsNotFoundResultWhenProductNotFound()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() }
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }
        [Fact]
        public async void PutValidateProductAttributeReturnBadRequestObjectResultWhenCategoryNotExist()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Provider());
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void PutValidateProductAttributeReturnBadRequestObjectResultWhenProviderNotExist()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            _repo.Setup(x => x.Provider.GetProviderAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(value: null);
            _repo.Setup(x => x.Category.GetCategoryAsync(It.IsAny<int>(), It.IsAny<bool>())).ReturnsAsync(new Category());
            var args = new Dictionary<string, object>() {
                { "id", 1 } ,
                { "product", new ProductIncomingDto() },
            };
            SetActionExecutingContext("PUT", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateProductAttributeInvokeNext()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void GetValidateProductAttributeReturnsNotFoundResultWhenProductNotFound()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void GetValidateProductAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("GET", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateProductAttributeInvokeNext()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }

        [Fact]
        public async void DeleteValidateProductAttributeReturnUnprocessableEntityObjectResultWhenModelInvalid()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(new Product());
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", true, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<UnprocessableEntityObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void DeleteValidateProductAttributeReturnsNotFoundResultWhenProductNotFound()
        {
            _repo.Setup(x => x.Product.GetProductAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<double>())).ReturnsAsync(value: null);
            var args = new Dictionary<string, object>() { { "id", 1 } };
            SetActionExecutingContext("DELETE", false, args);
            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<NotFoundResult>(_actionExecutingContext.Result);
        }

    }
}
