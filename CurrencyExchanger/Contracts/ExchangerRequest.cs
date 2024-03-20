namespace CurrencyExchanger.Contracts
{
    public record ExchangerRequest(string baseCurrencyCode, string targetCurrencyCode, decimal rate)
    {
    }
}
