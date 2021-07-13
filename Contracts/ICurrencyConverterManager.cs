using System.Collections.Generic;
using System.Linq;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts
{
    public interface ICurrencyConverterManager
    {
        public IEnumerable<Product> ChangeCurrency(IEnumerable<Product> products, string currencyName);

        public Product ChangeCurrency(Product products, string currencyName);
    }
}
