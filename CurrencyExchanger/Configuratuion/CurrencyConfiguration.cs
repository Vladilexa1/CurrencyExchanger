using CurrencyExchanger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchanger.Configuratuion
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<CurrencyEntity>
    {
        public void Configure(EntityTypeBuilder<CurrencyEntity> builder)
        {
            builder
                .HasKey(c => c.Id);
            builder
                .HasIndex(c => c.Code)
                .IsUnique();
            builder
                .HasMany(c => c.ExchangeRates)
                .WithOne(e => e.BaseCurrecy)
                .HasForeignKey(c => c.BaseCurrencyId);
        }
    }
}
