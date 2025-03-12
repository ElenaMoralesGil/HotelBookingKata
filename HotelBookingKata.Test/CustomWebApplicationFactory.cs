using HotelBookingKata.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingKata.Test;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>  where TProgram : class{

    public static Dictionary<string, InMemoryHotelRepository> TestHotelRepositories = new Dictionary<string, InMemoryHotelRepository>();
    public static Dictionary<string, InMemoryCompanyRepository> TestCompanyRepositories = new Dictionary<string, InMemoryCompanyRepository>();

    public string TestId { get; } = Guid.NewGuid().ToString();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment("Testing");

        if (!TestHotelRepositories.TryGetValue(TestId, out _ )){
            TestHotelRepositories[TestId] = new InMemoryHotelRepository();
        }

        if (!TestCompanyRepositories.TryGetValue(TestId, out _))
        {
            TestCompanyRepositories[TestId] = new InMemoryCompanyRepository();
        }

        builder.ConfigureServices(services =>
        {
           var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(HotelRepository));
            if (descriptor != null) services.Remove(descriptor);
            services.AddSingleton<HotelRepository>(TestHotelRepositories[TestId]);

            var descriptor2 = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(CompanyRepository));
            if (descriptor2 != null) services.Remove(descriptor2);
            services.AddSingleton<CompanyRepository>(TestCompanyRepositories[TestId]);

        });
    }

    public InMemoryHotelRepository GetRepository()
    {
        return TestHotelRepositories[TestId];
    }

    public InMemoryCompanyRepository GetCompanyRepository()
    {
        return TestCompanyRepositories[TestId];
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            TestHotelRepositories.Remove(TestId);
            TestCompanyRepositories.Remove(TestId);
        }
        base.Dispose(disposing);
    }
}