using Contracts;
using CurrencyApiConnections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;

namespace CurrencyConverter
{
    public class CurrencyApiConnection : ICurrencyApiConnection
    {
        public double GetExchangeRate(string currencyName)
        {
            using (var webClient = new WebClient())
            {
                //var url = @"https://exchange-rates.abstractapi.com/v1/live/?api_key=0996a12ad2d0439194a8d0780927ca10&base=USD";
                var url = @"https://www.nbrb.by/api/exrates/rates?periodicity=0";

                var jsonResponse = webClient.DownloadString(url);
                var deserializedResponse = JsonSerializer.Deserialize<List<CurrencyNBRB>>(jsonResponse);

                if (currencyName != null)
                {
                    var requiredCurrency = deserializedResponse.FirstOrDefault(c => c.Abbreviation == currencyName.ToUpper());
                    return (requiredCurrency?.OfficialRate / requiredCurrency?.Scale) ?? default(double);
                }

                return default(double);
            }
        }
    }
}
