using CurrencyExchanger.Data;
using CurrencyExchanger.Services;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchanger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
                builder => builder
                //.WithOrigins("http://localhost:52223")
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                ));
            builder.Services.AddControllers();

            builder.Services.AddDbContext<CurrenciesExchangerContext>(options =>
            {
                options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
            });
          
            builder.Services.AddScoped<ICurrencyRepository, CurrenciesExchangerRepository>();
            builder.Services.AddScoped<IExchangerRepository, CurrenciesExchangerRepository>();
            builder.Services.AddScoped<ICurrenciesService, CurrenciesService>();
            builder.Services.AddScoped<IExchangeService, ExchangeService>();

            var app = builder.Build();

            using var scope = app.Services.CreateScope();

            // Configure the HTTP request pipeline.
            app.UseCors("CorsPolicy");
           
            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.MapControllers();
            
            app.Run();
        }
    }
}
