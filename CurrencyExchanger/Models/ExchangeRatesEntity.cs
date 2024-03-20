namespace CurrencyExchanger.Models
{
    public class ExchangeRatesEntity
    {
        public int Id { get; set; }
        public int BaseCurrencyId { get; set; } 
        public CurrencyEntity? BaseCurrecy { get; set; }
        public int TargetCurrencyId { get; set; }
        public CurrencyEntity? TargetCurrency { get; set; }
        public decimal Rate { get; set; } = 0;
    }
}
