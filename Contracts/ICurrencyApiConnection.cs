namespace Contracts
{
    public interface ICurrencyApiConnection
    {
        public double GetExchangeRate(string currencyName);
    }
}
