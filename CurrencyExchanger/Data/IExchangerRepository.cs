using CurrencyExchanger.Models;

namespace CurrencyExchanger.Data
{
    public interface IExchangerRepository
    {
        Task<List<ExchangeRatesEntity>> GetExchangeRatesAsync();
        Task<ExchangeRatesEntity> GetExchangePairByCodeAsync(string baseCurrencyCode, string targetCurrencyCode);
        Task InsertExchangePairAsync(string baseCurrencyCode, string targetCurrencyCode, decimal rate);
        Task<ExchangeRatesEntity> UpdateExchangePairAsync(string baseCurrencyCode, string targetCurrencyCode, decimal rate);
        Task SaveAsync();
    }
}
