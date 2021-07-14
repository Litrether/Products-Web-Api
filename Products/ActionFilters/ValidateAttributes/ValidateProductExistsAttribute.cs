﻿using System.Threading.Tasks;
using Contracts;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Products.ActionFilters
{
    public class ValidateProductExistsAttribute : IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly ICurrencyConnection _currencyConnection;

        public ValidateProductExistsAttribute(IRepositoryManager repository,
            ILoggerManager logger, ICurrencyConnection currencyConnection)
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

            var currencyName = (context.ActionArguments["productParameters"] as ProductParameters).Currency;

            var exchangeRate = _currencyConnection.GetExchangeRate(currencyName);

            var product = await _repository.Product.GetProductAsync(id, trackChanges, exchangeRate);
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
