using CurrencyExchanger.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchanger.Configuratuion
{
    public class ExchangeRatesConfiguration : IEntityTypeConfiguration<ExchangeRatesEntity>
    {
        public void Configure(EntityTypeBuilder<ExchangeRatesEntity> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .HasIndex(e => new { e.BaseCurrencyId, e.TargetCurrencyId })
                .IsUnique();
            builder
                .HasOne(e => e.BaseCurrecy)
                .WithMany(c => c.ExchangeRates);
        }
    }
}
