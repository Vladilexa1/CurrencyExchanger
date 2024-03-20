using CurrencyExchanger.Models;

namespace CurrencyExchanger.Contracts
{
    public record CurrencyResponse(int id, string name, string code, string sign)
    {

    }
    
}
