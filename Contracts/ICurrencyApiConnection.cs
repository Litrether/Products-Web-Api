using System.Collections.Generic;
using Entities.Models;

namespace Contracts
{
    public interface ICurrencyApiConnection
    {
        public decimal GetExchangeRate(string currencyName);
    }
}
