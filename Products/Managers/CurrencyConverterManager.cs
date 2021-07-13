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

        public CurrencyConverterManager(ILoggerManager logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }


        public IEnumerable<Product> ChangeCurrency(IEnumerable<Product> products, string currencyName)
        {
            if (currencyName == null)
                return products;

            var url = string.Concat(_configuration.GetSection("CurrencyWebApi").Value, currencyName);

            var exchangeRate = GetExchangeRate(url, currencyName);
            if (exchangeRate == 1)
                return products;

            var changedProductsDto = ConvertCurrency(products, exchangeRate);

            return changedProductsDto;
        }

        public Product ChangeCurrency(Product product, string currencyName)
        {
            if (currencyName == null)
                return product;

            var url = string.Concat(_configuration.GetSection("CurrencyWebApi").Value, currencyName);

            var exchangeRate = GetExchangeRate(url, currencyName);
            if (exchangeRate == 1)
                return product;

            var changedProductDto = ConvertCurrencyForEntities(product, exchangeRate);

            return changedProductDto;
        }

        private decimal GetExchangeRate(string url, string currencyName)
        {
            string jsonResponse;
            try
            {
                jsonResponse = new WebClient().DownloadString(url);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return 1;
            }

            var response = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, string>>>(jsonResponse);

            //var dynamicExchangeRate = new JavaScriptSerializer().Deserialize<dynamic>(jsonExchangeRate);

            //var result = (decimal)dynamicExchangeRate["exchange_rates"][currencyName.ToUpper()];

            //if (result == 0) {
            //    _logger.LogError($"Currency doesn't exists in the Web Api. Url: {url}");
            //    return 1;
            //}

            var result = decimal.Parse(response["exchange_rates"][currencyName.ToUpper()]);

            _logger.LogInfo(result.ToString());

            return result;
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
