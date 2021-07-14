using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ICurrencyConnection
    {
        public decimal GetExchangeRate(string currencyName);
    }
}
