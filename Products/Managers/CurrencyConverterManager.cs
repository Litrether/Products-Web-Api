using System.Net;
using Contracts;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Collections.Generic;
using Entities.Models;
using System;
using Newtonsoft.Json;

namespace Products.Managers
{
    public class CurrencyConverterManager : ICurrencyConverterManager
    {
        private readonly ILoggerManager _logger;
        private readonly IConfiguration _configuration;
        private readonly ICurrencyConnection _connection;

        public CurrencyConverterManager(ILoggerManager logger,
            IConfiguration configuration, ICurrencyConnection connection)
        {
            _logger = logger;
            _configuration = configuration;
            _connection = connection;
        }

        public IEnumerable<Product> ChangeCurrency(IEnumerable<Product> products, string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
                return products;

            decimal exchangeRate;
            try
            {
                exchangeRate = _connection.GetExchangeRate(currencyName);
            }
            catch
            {
                _logger.LogError("Bad connection to outside Currency Web Api");
                throw new Exception("Bad connection to outside Currency Web Api");
            }

            var resultProducts = ConvertCurrency(products, exchangeRate);

            return resultProducts;
        }

        public Product ChangeCurrency(Product product, string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
                return product;

            decimal exchangeRate;
            try
            {
                exchangeRate = _connection.GetExchangeRate(currencyName);
            }
            catch
            {
                _logger.LogError("Bad connection to outside Currency Web Api");
                throw new Exception("Bad connection to outside Currency Web Api");
            }


            var resultProduct = ConvertCurrencyForEntities(product, exchangeRate);

            return resultProduct;
        }

        private IEnumerable<Product> ConvertCurrency(IEnumerable<Product> products,
            decimal exchangeRate)
        {
            products.AsParallel().ForAll(p => ConvertCurrencyForEntities(p, exchangeRate));

            return products;
        }

        private Product ConvertCurrencyForEntities(Product products, decimal exchangeRate)
        {
            products.Cost *= exchangeRate;

            return products;
        }
    }
}
