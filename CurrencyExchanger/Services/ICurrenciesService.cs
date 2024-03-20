using CurrencyExchanger.Contracts;
using CurrencyExchanger.Models;

namespace CurrencyExchanger.Services
{
    public interface ICurrenciesService
    {
        Task<CurrencyEntity> AddNewCurenncy(string code, string fullName, string sign);
        Task<List<CurrencyEntity>> GetAllCurencies();
        Task<CurrencyEntity> GetCurenncy(string code);
        Task<CurrencyEntity> GetCurenncy(int id);
        CurrencyResponse BildCurrencyResponse(CurrencyEntity currencyEntity);
    }
}