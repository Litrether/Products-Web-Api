using Contracts;
using System;
using System.Net;
using System.Text.Json;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyApiConnectionERA : ICurrencyApiConnection
    {

        public decimal GetExchangeRate(string currencyName)
        {
            var url = @"https://exchange-rates.abstractapi.com/v1/live/?api_key=f775232ebe7c4e3793a4fe6b73f4295f&base=USD";

            var jsonResponse = new WebClient().DownloadString(url);

            var deserializedResponse = JsonSerializer.Deserialize<CurrencyERA>(jsonResponse);

            if (currencyName != null && deserializedResponse.ExchangeRates.ContainsKey(currencyName.ToUpper()) == false )
                throw new Exception("Currency name doesn't exist in the external Web Api!");

            return 1;
        }
    }
}
