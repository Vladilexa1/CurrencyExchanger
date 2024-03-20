using CurrencyExchanger.Contracts;
using CurrencyExchanger.Data;
using CurrencyExchanger.Models;

namespace CurrencyExchanger.Services
{
    public class CurrenciesService : ICurrenciesService
    {
        private readonly ICurrencyRepository repository;
        public CurrenciesService(ICurrencyRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<CurrencyEntity>> GetAllCurencies()
        {
            return await repository.GetCurrenciesAsync();
        }
        public async Task<CurrencyEntity> GetCurenncy(string code)
        {
            return await repository.GetCurrencyAsync(code);
        }
        public async Task<CurrencyEntity> AddNewCurenncy(string code, string fullName, string sign)
        {
            var newCurrency = new CurrencyEntity
            {
                Code = code,
                FullName = fullName,
                Sign = sign
            };
            await repository.InsertCurrencyAsync(newCurrency);
            return newCurrency;
        }
        public async Task<CurrencyEntity> GetCurenncy(int id)
        {
            return await repository.GetCurrencyByIdAsync(id);
        }
        public CurrencyResponse BildCurrencyResponse(CurrencyEntity currencyEntity)
        {
            return new CurrencyResponse(currencyEntity.Id, currencyEntity.FullName, currencyEntity.Code, currencyEntity.Sign);
        }
    }
}
