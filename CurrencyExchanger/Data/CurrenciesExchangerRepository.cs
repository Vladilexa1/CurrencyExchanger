using CurrencyExchanger.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace CurrencyExchanger.Data
{
    public class CurrenciesExchangerRepository : ICurrencyRepository, IExchangerRepository
    {
        private readonly CurrenciesExchangerContext _dbContext;
        public CurrenciesExchangerRepository(CurrenciesExchangerContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Task<List<CurrencyEntity>> GetCurrenciesAsync() 
        {
            return _dbContext.Currencies
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<CurrencyEntity> GetCurrencyAsync(string code)
        {
           return await _dbContext.Currencies
                .AsNoTracking()
                .Where(c => c.Code == code)
                .FirstOrDefaultAsync() ?? throw new SqlNullValueException();
        }
        public async Task InsertCurrencyAsync(CurrencyEntity currency) 
        {
            await _dbContext.Currencies.AddAsync(currency);
            await _dbContext.SaveChangesAsync();
        }
        public async Task UpdateCurrencyAsync(CurrencyEntity currency)
        {
            await _dbContext.Currencies
                .Where(c => c.Id == currency.Id)
                .ExecuteUpdateAsync(c => c
                .SetProperty(c => c.Id, currency.Id)
                .SetProperty(c => c.FullName, currency.FullName)
                .SetProperty(c => c.Sign, currency.Sign));
        }
        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<ExchangeRatesEntity>> GetExchangeRatesAsync()
        {
           return await _dbContext.ExchangeRates.ToListAsync();
        }
        public async Task<ExchangeRatesEntity> GetExchangePairByCodeAsync(string baseCurrencyCode, string targetCurrencyCode)
        {
            var query = await _dbContext.ExchangeRates.Where(e =>
            e.BaseCurrecy.Code == baseCurrencyCode &&
            e.TargetCurrency.Code == targetCurrencyCode)
                .FirstOrDefaultAsync();
            if (query is null) return null;
            query.BaseCurrecy = await GetCurrencyAsync(baseCurrencyCode);
            query.TargetCurrency = await GetCurrencyAsync(targetCurrencyCode);
            return query;
        }

        public async Task InsertExchangePairAsync(string baseCurrencyCode, string targetCurrencyCode, decimal rate)
        {
            var baseCurrency = await GetCurrencyAsync(baseCurrencyCode);
            var targetCurrency = await GetCurrencyAsync(targetCurrencyCode);
            var newPair = new ExchangeRatesEntity
            {
                BaseCurrencyId = baseCurrency.Id,
                TargetCurrencyId = targetCurrency.Id,
                Rate = rate
            };
            await _dbContext.ExchangeRates.AddAsync(newPair);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<ExchangeRatesEntity> UpdateExchangePairAsync(string baseCurrencyCode, string targetCurrencyCode, decimal rate)
        {
            var baseCurrency = await GetCurrencyAsync(baseCurrencyCode);
            var targetCurrency = await GetCurrencyAsync(targetCurrencyCode);

            var query = await _dbContext.ExchangeRates.FirstOrDefaultAsync(e =>
              e.BaseCurrecy.Id == baseCurrency.Id &&
              e.TargetCurrency.Id == targetCurrency.Id) ?? throw new SqlNullValueException();
            query.Rate = rate;
            query.BaseCurrecy = baseCurrency;
            query.TargetCurrency = targetCurrency;
            await _dbContext.SaveChangesAsync();
            return query;
        }
        public async Task<CurrencyEntity> GetCurrencyByIdAsync(int id)
        {
           return await _dbContext.Currencies.Where(c => c.Id == id).FirstOrDefaultAsync() ?? throw new Exception();
        }
    }
}
