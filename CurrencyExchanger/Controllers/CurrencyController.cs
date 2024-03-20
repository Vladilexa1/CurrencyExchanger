using CurrencyExchanger.Data;
using CurrencyExchanger.Models;
using CurrencyExchanger.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using CurrencyExchanger.Contracts;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchanger.Controllers
{
    [ApiController]
    [Route("/currencies")]
    public class CurrencyController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<CurrencyResponse>>> GetAll(ICurrenciesService service)
        {
            List<CurrencyEntity> currencyEntities = new();
            try
            {
                 currencyEntities = await service.GetAllCurencies();
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }

            var request = new List<CurrencyResponse>();
            foreach (var c in currencyEntities)
            {
                request.Add(service.BildCurrencyResponse(c));
            }
            return Ok(request);
        }
        [HttpGet("{Code}")]
        public async Task<ActionResult<CurrencyResponse>> GetOne(string code, ICurrenciesService service)
        {
            try
            {
                if (String.IsNullOrEmpty(code))
                    return StatusCode(400, new MessageRequest("The currency code is missing in the address"));
                var currencies = await service.GetCurenncy(code);
                if (currencies is null)
                    return StatusCode(404, new MessageRequest("Currency not found"));
                var response = service.BildCurrencyResponse(currencies);
                return Ok(response);
            }
            catch (Exception) 
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create([FromForm] CurrencyRequest currencies, ICurrenciesService service)
        {
            if (String.IsNullOrEmpty(currencies.code) 
                || String.IsNullOrEmpty(currencies.name) 
                || String.IsNullOrEmpty(currencies.sign))
                return StatusCode(400, new MessageRequest("A required form field is missing"));
            
            try
            {
                await service.AddNewCurenncy(
                    currencies.code,
                    currencies.name,
                    currencies.sign);
            }
            catch (DbUpdateException)
            {
                return StatusCode(409, new MessageRequest("A currency with this code already exists"));
            }
            catch (Exception)
            {
                return StatusCode(500, new MessageRequest("Database is not aviable"));
            }
            return StatusCode(201);
        }
    }

}
