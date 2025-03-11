using HotelBookingKata.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingKata.Test;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>  where TProgram : class{

    public static Dictionary<string, InMemoryHotelRepository> TestRepositories = new Dictionary<string, InMemoryHotelRepository>();
    public string TestId { get; } = Guid.NewGuid().ToString();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment("Testing");

        if (!TestRepositories.TryGetValue(TestId, out _ )){
            TestRepositories[TestId] = new InMemoryHotelRepository();
        }

        builder.ConfigureServices(services =>
        {
           var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(HotelRepository));
            if (descriptor != null) services.Remove(descriptor);
            services.AddSingleton<HotelRepository>(TestRepositories[TestId]);
        });
    }

    public InMemoryHotelRepository GetRepository()
    {
        return TestRepositories[TestId];
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            TestRepositories.Remove(TestId);
        }
        base.Dispose(disposing);
    }
}