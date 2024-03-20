using CurrencyExchanger.Configuratuion;
using CurrencyExchanger.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchanger.Data
{
    public class CurrenciesExchangerContext(DbContextOptions<CurrenciesExchangerContext> options) : DbContext(options)
    {
        public DbSet<CurrencyEntity> Currencies { get; set; }
        public DbSet<ExchangeRatesEntity> ExchangeRates { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new ExchangeRatesConfiguration());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
