using CurrencyExchanger.Models;

namespace CurrencyExchanger.Services
{
    public interface IExchangeService
    {
        Task AddNewExchangeRates(string baseCurrency, string targetCurrency, decimal rate);
        Task<List<ExchangeRatesEntity>> GetAllExchangeRates();
        Task<ExchangeRatesEntity> GetOneExchangeRates(string exchangePair);
        Task<ExchangeRatesEntity> GetExchange(string from, string to, string amount);
        Task<ExchangeRatesEntity> Update(string pair, decimal rate);
    }
}