using System.Net;
using System.Text.Json;
using Contracts;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyApiConnectionERA : ICurrencyApiConnection
    {

        public double GetExchangeRate(string currencyName)
        {
            var url = @"https://exchange-rates.abstractapi.com/v1/live/?api_key=0996a12ad2d0439194a8d0780927ca10&base=USD";

            string jsonResponse;

            using(var webClient = new WebClient())
            {
                jsonResponse = webClient.DownloadString(url);
            }

            var deserializedResponse = JsonSerializer.Deserialize<CurrencyERA>(jsonResponse);

            if (currencyName != null && deserializedResponse.ExchangeRates.ContainsKey(currencyName.ToUpper()))
                return deserializedResponse.ExchangeRates[currencyName.ToUpper()];

            return default(double);
        }
    }
}
