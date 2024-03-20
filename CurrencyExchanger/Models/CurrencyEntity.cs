﻿namespace CurrencyExchanger.Models
{
    public class CurrencyEntity
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Sign { get; set; } = string.Empty;
        public List<ExchangeRatesEntity> ExchangeRates { get; set; } = [];
    }
}
