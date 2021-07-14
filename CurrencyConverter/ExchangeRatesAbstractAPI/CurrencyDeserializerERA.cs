using System;
using System.Text.Json;
using Contracts;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyDeserializerERA : ICurrencyDeserializer
    {
        public decimal Deserialize(string jsonResponse, string currencyName)
        {
            var deserializedResponse = JsonSerializer.Deserialize<CurrencyERA>(jsonResponse); 

            var result = (decimal)deserializedResponse.ExchangeRates[currencyName.ToUpper()];
                
            return result;
        }
    }
}
