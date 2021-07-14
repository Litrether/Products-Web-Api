using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ICurrencyDeserializer
    {
        public decimal Deserialize(string jsonResponse, string currencyName);
    }
}
