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
        private readonly ICurrencyDeserializer _currencyDeserializer;

        public CurrencyConverterManager(ILoggerManager logger,
            IConfiguration configuration, ICurrencyDeserializer currencyDeserializer)
        {
            _logger = logger;
            _configuration = configuration;
            _currencyDeserializer = currencyDeserializer;
        }

        public IEnumerable<Product> ChangeCurrency(IEnumerable<Product> products, string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
                return products;

            var exchangeRate = GetJsonResponse(currencyName);
            var resultProducts = ConvertCurrency(products, exchangeRate);

            return resultProducts;
        }

        public Product ChangeCurrency(Product product, string currencyName)
        {
            if (string.IsNullOrWhiteSpace(currencyName))
                return product;

            var exchangeRate = GetJsonResponse(currencyName);
            var resultProduct = ConvertCurrencyForEntities(product, exchangeRate);

            return resultProduct;
        }

        private decimal GetJsonResponse(string currencyName)
        {
            var url = _configuration.GetSection("CurrencyWebApi").Value;

            string jsonResponse = null;
            try
            {
                jsonResponse = new WebClient().DownloadString(url);
            }
            catch
            {
                _logger.LogError("Server doesn't have connection to the outside Currency Web Api.");
            }

            decimal exchangeRate = 1;
            try
            {
                exchangeRate = _currencyDeserializer.Deserialize(jsonResponse, currencyName);
            }
            catch
            {
                _logger.LogError($"Currency \"{currencyName}\" doesn't exists in the outside Currency Web Api.");
            }

            return exchangeRate;
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
