﻿using System;
using System.Net;
using System.Text.Json;
using Contracts;

namespace CurrencyConverter.ExchangeRatesAbstractAPI
{
    public class CurrencyConnectionERA : ICurrencyConnection
    {
        public decimal GetExchangeRate(string currencyName)
        {
            var url = @"https://exchange-rates.abstractapi.com/v1/live/?api_key=f775232ebe7c4e3793a4fe6b73f4295f&base=USD";

            var jsonResponse = new WebClient().DownloadString(url);

            var deserializedResponse = JsonSerializer.Deserialize<CurrencyERA>(jsonResponse); 

            var result = (decimal)deserializedResponse.ExchangeRates[currencyName.ToUpper()];
                
            return result;
        }
    }
}