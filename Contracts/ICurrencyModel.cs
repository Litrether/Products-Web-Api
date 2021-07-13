using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface ICurrencyModel
    {
        //todo сделать модель currency

        public string CurrencyName { get; set; }

        public decimal ExchangeRate { get; set; }
    }
}
