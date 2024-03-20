namespace CurrencyExchanger.Contracts
{
    public record ExchangerResponse(int id, CurrencyResponse baseCurrency, CurrencyResponse targetCurrency, decimal rate)
    {
    }
}
