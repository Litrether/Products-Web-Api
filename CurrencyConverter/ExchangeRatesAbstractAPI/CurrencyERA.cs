using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyERA
    {
        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("last_updated")]
        public long LastUpdated { get; set; }

        [JsonPropertyName("exchange_rates")]
        public Dictionary<string, double> ExchangeRates { get; set; }
    }
}
