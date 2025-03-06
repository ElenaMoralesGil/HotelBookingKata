using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingKata.Test;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>  where TProgram : class{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
            {
                services.AddControllers();
                services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
                services.AddScoped<HotelService>();
                
            });

            builder.UseEnvironment("Testing");
    }
}