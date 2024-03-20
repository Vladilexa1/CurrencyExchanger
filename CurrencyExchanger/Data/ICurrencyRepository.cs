using CurrencyExchanger.Models;

namespace CurrencyExchanger.Data
{
    public interface ICurrencyRepository
    {
        Task<List<CurrencyEntity>> GetCurrenciesAsync();
        Task<CurrencyEntity> GetCurrencyByIdAsync(int id);
        Task<CurrencyEntity> GetCurrencyAsync(string code);
        Task InsertCurrencyAsync(CurrencyEntity currenciesModel);
        Task UpdateCurrencyAsync(CurrencyEntity currenciesModel);
        Task SaveAsync();
    }
}
