using Contracts;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace Products.ActionFilters
{
    public class ValidateProductExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly ICurrencyApiConnection _currencyConnection;

        public ValidateProductExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger, ICurrencyApiConnection currencyConnection)
        {
            _repository = repository;
            _logger = logger;
            _currencyConnection = currencyConnection;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;

            var trackChanges =
                (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            var id = (int)context.ActionArguments["id"];

            decimal exchangeRate = 1;
            if (context.ActionArguments.ContainsKey("productParameters"))
            {
                var currencyName = (context.ActionArguments["productParameters"] as ProductParameters).Currency;
                exchangeRate = _currencyConnection.GetExchangeRate(currencyName);
            }

            Product product;
            try
            {
                product = await _repository.Product.GetProductAsync(id, trackChanges, exchangeRate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                context.Result = new BadRequestObjectResult(ex.Message);
                return;
            }
            if (product == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");

                context.Result = new NotFoundResult();
                return;
            }

            context.HttpContext.Items.Add("product", product);

            await next();
        }
    }
}
