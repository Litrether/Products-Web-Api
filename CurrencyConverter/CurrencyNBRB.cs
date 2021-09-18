using System;
using System.Text.Json.Serialization;

namespace CurrencyApiConnections
{
    public class CurrencyNBRB
    {
        [JsonPropertyName("Cur_ID")]
        public int Id { get; set; }

        [JsonPropertyName("Date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("Cur_Abbreviation")]
        public string Abbreviation { get; set; }

        [JsonPropertyName("Cur_Scale")]
        public int Scale { get; set; }

        [JsonPropertyName("Cur_Name")]
        public string Name { get; set; }

        [JsonPropertyName("Cur_OfficialRate")]
        public double OfficialRate { get; set; }
    }
}
