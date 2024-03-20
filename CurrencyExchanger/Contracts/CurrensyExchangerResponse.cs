namespace CurrencyExchanger.Contracts
{
    public record CurrensyExchangerResponse
        (CurrencyResponse baseCurrency, CurrencyResponse targetCurrency, decimal rate, decimal ammount, decimal convertedAmount)
    {
    }
}
