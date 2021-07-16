namespace Contracts
{
    public interface ICurrencyApiConnection
    {
        public decimal GetExchangeRate(string currencyName);
    }
}
