using HotelBookingKata.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
namespace HotelBookingKata.Test;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram>  where TProgram : class{

    public static Dictionary<string, InMemoryHotelRepository> TestHotelRepositories = new Dictionary<string, InMemoryHotelRepository>();
    public static Dictionary<string, InMemoryCompanyRepository> TestCompanyRepositories = new Dictionary<string, InMemoryCompanyRepository>();
    public static Dictionary<string, InMemoryEmployeeRepository> TestEmployeeRepositories = new Dictionary<string, InMemoryEmployeeRepository>();
    public static Dictionary<string, InMemoryBookingPolicyRepository> TestBookingPolicyRepositories = new Dictionary<string, InMemoryBookingPolicyRepository>();
    public static Dictionary<string, InMemoryBookingRepository> TestBookingRepositories = new Dictionary<string, InMemoryBookingRepository>();


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

        if (!TestEmployeeRepositories.TryGetValue(TestId, out _))
        {
            TestEmployeeRepositories[TestId] = new InMemoryEmployeeRepository();
        }

        if(!TestBookingPolicyRepositories.TryGetValue(TestId, out _))
        {
            TestBookingPolicyRepositories[TestId] = new InMemoryBookingPolicyRepository();
        }

        if(!TestBookingRepositories.TryGetValue(TestId, out _))
        {
            TestBookingRepositories[TestId] = new InMemoryBookingRepository();
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

            var descriptor3 = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(EmployeeRepository));
            if (descriptor3 != null) services.Remove(descriptor3);
            services.AddSingleton<EmployeeRepository>(TestEmployeeRepositories[TestId]);

            var descriptor4 = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(BookingPolicyRepository));
            if (descriptor4 != null) services.Remove(descriptor4);
            services.AddSingleton<BookingPolicyRepository>(TestBookingPolicyRepositories[TestId]);

            var descriptor5 = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(BookingRepository));
            if (descriptor5 != null) services.Remove(descriptor5);
            services.AddSingleton<BookingRepository>(TestBookingRepositories[TestId]);

        });
    }

    public InMemoryHotelRepository GetHotelRepository()
    {
        return TestHotelRepositories[TestId];
    }

    public InMemoryCompanyRepository GetCompanyRepository()
    {
        return TestCompanyRepositories[TestId];
    }

    public InMemoryEmployeeRepository GetEmployeeRepository()
    {
        return TestEmployeeRepositories[TestId];
    }

    public InMemoryBookingPolicyRepository GetBookingPolicyRepository()
    {
        return TestBookingPolicyRepositories[TestId];
    }

    public InMemoryBookingRepository GetBookingRepository()
    {
        return TestBookingRepositories[TestId];
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            TestHotelRepositories.Remove(TestId);
            TestCompanyRepositories.Remove(TestId);
            TestEmployeeRepositories.Remove(TestId);
            TestBookingPolicyRepositories.Remove(TestId);
            TestBookingRepositories.Remove(TestId);
        }
        base.Dispose(disposing);
    }
}