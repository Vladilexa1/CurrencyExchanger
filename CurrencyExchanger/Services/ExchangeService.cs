using CurrencyExchanger.Data;
using CurrencyExchanger.Models;

namespace CurrencyExchanger.Services
{
    public class ExchangeService : IExchangeService
    {
        private readonly IExchangerRepository repository;
        public ExchangeService(IExchangerRepository repository)
        {
            this.repository = repository;
        }
        public async Task<List<ExchangeRatesEntity>> GetAllExchangeRates()
        {
            return await repository.GetExchangeRatesAsync();
        }
        public async Task<ExchangeRatesEntity> GetOneExchangeRates(string exchangePair)
        {
            var splitCurrensy = SplitPairCurensy(exchangePair);
            var baseCurrency = splitCurrensy[0];
            var targetCurrency = splitCurrensy[1];
            return await repository.GetExchangePairByCodeAsync(baseCurrency, targetCurrency);
        }
        public async Task AddNewExchangeRates(string baseCurrency, string targetCurrency, decimal rate)
        {
            await repository.InsertExchangePairAsync(baseCurrency, targetCurrency, rate);
        }
        private string[] SplitPairCurensy(string exchangePair)
        {
            var charArray = exchangePair.ToCharArray();
            var baseCurrency = string.Empty;
            var targetCurrency = string.Empty;
            foreach (var c in charArray)
            {
                if (baseCurrency.Length != 3)
                {
                    baseCurrency += c;
                }
                else
                {
                    targetCurrency += c;
                }
            }
            string[] result = [baseCurrency, targetCurrency];
            return result;
        }
        public async Task<ExchangeRatesEntity> GetExchange(string from, string to, string amount)
        {
            //Direct course
            var pair = await repository.GetExchangePairByCodeAsync(from, to); 
            
            if (pair is null) 
            {
                // reverse course
                pair = await repository.GetExchangePairByCodeAsync(to, from);
                if (pair != null)
                {
                    pair.Rate = Math.Round(1 / pair.Rate, 6);
                    return pair;
                }
                else // cross course
                {
                    var fromUSD = await repository.GetExchangePairByCodeAsync("USD", from);
                    var USDto = await repository.GetExchangePairByCodeAsync("USD", to);
                    var result = new ExchangeRatesEntity
                    {
                        BaseCurrecy = fromUSD.TargetCurrency,
                        TargetCurrency = USDto.TargetCurrency,
                        Rate = Math.Round(USDto.Rate / fromUSD.Rate, 6)
                    };
                    return result;
                }
            }
            return pair;
        }
        public async Task<ExchangeRatesEntity> Update(string pair, decimal rate)
        {
            var currencyArray = SplitPairCurensy(pair);
            return await repository.UpdateExchangePairAsync(currencyArray[0], currencyArray[1], rate);
        }
    }
}
