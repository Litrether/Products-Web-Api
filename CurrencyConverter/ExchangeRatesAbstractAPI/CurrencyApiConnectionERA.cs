using System.Net;
using System.Text.Json;
using Contracts;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyApiConnectionERA : ICurrencyApiConnection
    {

        public double GetExchangeRate(string currencyName)
        {
            var url = @"https://exchange-rates.abstractapi.com/v1/live/?api_key=f775232ebe7c4e3793a4fe6b73f4295f&base=USD";

            string jsonResponse;
            try
            {
                jsonResponse = new WebClient().DownloadString(url);
            }
            catch (System.Exception)
            {
                return default(double);
            }

            var deserializedResponse = JsonSerializer.Deserialize<CurrencyERA>(jsonResponse);

            if (currencyName != null && deserializedResponse.ExchangeRates.ContainsKey(currencyName.ToUpper()))
                return deserializedResponse.ExchangeRates[currencyName.ToUpper()];

            return default(double);
        }
    }
}
