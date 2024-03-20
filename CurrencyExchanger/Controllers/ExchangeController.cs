using CurrencyExchanger.Contracts;
using CurrencyExchanger.Data;
using CurrencyExchanger.Models;
using CurrencyExchanger.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlTypes;
using System.Text.Json;

namespace CurrencyExchanger.Controllers
{
    [ApiController]
    [Route("/exchangeRates")]
    public class ExchangeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllPair(IExchangeService service, ICurrenciesService currenciesService)
        {
            List<ExchangerResponse> requests = new();
            List<ExchangeRatesEntity> exchangeRates = new();
            try
            {
                exchangeRates = await service.GetAllExchangeRates();
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
            foreach (var e in exchangeRates)
            {
                var baseCurrensyEntity = await currenciesService.GetCurenncy(e.BaseCurrencyId);
                var baseCurrensy = currenciesService.BildCurrencyResponse(baseCurrensyEntity);

                var targetCurrensyEntity = await currenciesService.GetCurenncy(e.TargetCurrencyId);
                var targetCurrensy = currenciesService.BildCurrencyResponse(targetCurrensyEntity);

                requests.Add(new ExchangerResponse(e.Id, baseCurrensy, targetCurrensy, e.Rate));
            }
            return Ok(requests);
        }
        [HttpGet("{CurrencyPair}")]
        public async Task<ActionResult> GetPair(string CurrencyPair, IExchangeService service, ICurrenciesService currenciesService)
        {
            ExchangeRatesEntity exchangeRate;
            if (String.IsNullOrEmpty(CurrencyPair)) 
                return StatusCode(400, new MessageRequest("The pair's currency codes are missing in the address"));
            try
            {
                exchangeRate = await service.GetOneExchangeRates(CurrencyPair);
                if (exchangeRate is null) return StatusCode(404, new MessageRequest("Exchange rate for pair not found"));
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
            var baseCurrensy = currenciesService.BildCurrencyResponse(exchangeRate.BaseCurrecy);
            var targetCurrensy = currenciesService.BildCurrencyResponse(exchangeRate.TargetCurrency);
            var response = new ExchangerResponse(exchangeRate.Id, baseCurrensy, targetCurrensy, exchangeRate.Rate);
            return Ok(response);
        }
        [HttpPost]
        public async Task<ActionResult> AddPair([FromForm] ExchangerRequest request, IExchangeService service)
        {

            if (String.IsNullOrEmpty(request.baseCurrencyCode)
                || String.IsNullOrEmpty(request.targetCurrencyCode)
                || request.rate == 0) 
                return StatusCode(400, new MessageRequest("A required form field is missing"));
            try
            {
                await service.AddNewExchangeRates(request.baseCurrencyCode, request.targetCurrencyCode, request.rate);
            }
            catch (DbUpdateException)
            {
                return StatusCode(409, new MessageRequest("A currency pair with this code already exists"));
            }
            catch (SqlNullValueException)
            {
                return StatusCode(404, new MessageRequest("One (or both) currencies from the currency pair does not exist in the database"));
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
            return Ok(201);
        }
        [HttpPatch("{pair}")]
        public async Task<ActionResult> UpdateExchangeRatesAsync(string pair, [FromForm] decimal rate, IExchangeService service, ICurrenciesService currenciesService)
        {
            ExchangeRatesEntity exchangeRates;
            if (String.IsNullOrEmpty(pair) || rate == 0) return StatusCode(400, new MessageRequest("A required form field is missing"));
            try
            {
                exchangeRates = await service.Update(pair, rate);
            }
            catch (SqlNullValueException)
            {
                return StatusCode(404, new MessageRequest("Currency pair is not in the database"));
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
            var baseCurrensy = currenciesService.BildCurrencyResponse(exchangeRates.BaseCurrecy);
            var targetCurrensy = currenciesService.BildCurrencyResponse(exchangeRates.TargetCurrency);
            var response = new ExchangerResponse(exchangeRates.Id, baseCurrensy, targetCurrensy, exchangeRates.Rate);
           
            return Ok(response);
        }
        [HttpGet("/exchange")]
        public async Task<ActionResult> GetCurrensyExchange(IExchangeService service, ICurrenciesService currenciesService,
            [FromQuery]string from, [FromQuery] string to, [FromQuery] string amount)
        {
            decimal.TryParse(amount, out decimal sum);
            var request = await service.GetExchange(from, to, amount);
            var convertedAmount = Math.Round(sum * request.Rate, 6);
            var result = new CurrensyExchangerResponse( 
                currenciesService.BildCurrencyResponse(request.BaseCurrecy),
                currenciesService.BildCurrencyResponse(request.TargetCurrency),
                request.Rate, sum, convertedAmount);
            
            return Ok(result);
        }
    }
}
