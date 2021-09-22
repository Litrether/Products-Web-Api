using Contracts;
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
    public class ValidateCartAttributeTests
    {
        Mock<IRepositoryManager> _repo = new Mock<IRepositoryManager>();
        Mock<ILoggerManager> _logger = new Mock<ILoggerManager>();
        private Mock<ActionExecutionDelegate> _next;
        private ActionExecutingContext _actionExecutingContext;
        private ValidateCartAttribute _filter;

        public ValidateCartAttributeTests()
        {
            var modelState = new ModelStateDictionary();
            var httpContext = new Mock<HttpContext>();
            var routeData = new Mock<RouteData>();
            var actionDescriptor = new Mock<ActionDescriptor>();

            var actionContext = new ActionContext(httpContext.Object, routeData.Object, actionDescriptor.Object, modelState);
            var args = new Dictionary<string, object>();
            args.Add("productId", 1);
            _actionExecutingContext = new ActionExecutingContext(
                    actionContext,
                    new List<IFilterMetadata>(),
                    args,
                    Mock.Of<Controller>()
                );

            _next = new Mock<ActionExecutionDelegate>();
            _filter = new ValidateCartAttribute(_repo.Object, _logger.Object);
        }

        [Fact]
        public async void ValidateCartAttributeReturnsBadRequestObjectResultWhenProductNotFound()
        {
            _repo.Setup(x => x.Product.GetProductAsync(
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<double>())
                ).ReturnsAsync(value: null);

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            Assert.IsType<BadRequestObjectResult>(_actionExecutingContext.Result);
        }

        [Fact]
        public async void ValidateCartAttributeInvokeNext()
        {
            _repo.Setup(x => x.Product.GetProductAsync(
                    It.IsAny<int>(),
                    It.IsAny<bool>(),
                    It.IsAny<double>())
                ).ReturnsAsync(new Product());

            await _filter.OnActionExecutionAsync(_actionExecutingContext, _next.Object);

            _next.Verify(x => x());
        }
    }
}
