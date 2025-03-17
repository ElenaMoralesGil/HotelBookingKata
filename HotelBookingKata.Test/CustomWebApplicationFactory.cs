using HotelBookingKata.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using HotelBookingKata.Common;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.Test;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{

    public static Dictionary<string, InMemoryHotelRepository> TestHotelRepositories = new Dictionary<string, InMemoryHotelRepository>();
    public static Dictionary<string, InMemoryCompanyRepository> TestCompanyRepositories = new Dictionary<string, InMemoryCompanyRepository>();
    public static Dictionary<string, InMemoryEmployeeRepository> TestEmployeeRepositories = new Dictionary<string, InMemoryEmployeeRepository>();
    public static Dictionary<string, InMemoryBookingPolicyRepository> TestBookingPolicyRepositories = new Dictionary<string, InMemoryBookingPolicyRepository>();
    public static Dictionary<string, InMemoryBookingRepository> TestBookingRepositories = new Dictionary<string, InMemoryBookingRepository>();


    public string TestId { get; } = Guid.NewGuid().ToString();
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {

        builder.UseEnvironment("Testing");

        TestHotelRepositories[TestId] = new InMemoryHotelRepository();
        TestCompanyRepositories[TestId] = new InMemoryCompanyRepository();
        TestEmployeeRepositories[TestId] = new InMemoryEmployeeRepository();
        TestBookingPolicyRepositories[TestId] = new InMemoryBookingPolicyRepository();
        TestBookingRepositories[TestId] = new InMemoryBookingRepository();

        builder.ConfigureServices(services =>
        {

            ReplaceProgramWithTestRepository<HotelRepository>(services, TestHotelRepositories[TestId]);
            ReplaceProgramWithTestRepository<CompanyRepository>(services, TestCompanyRepositories[TestId]);
            ReplaceProgramWithTestRepository<EmployeeRepository>(services, TestEmployeeRepositories[TestId]);
            ReplaceProgramWithTestRepository<BookingPolicyRepository>(services, TestBookingPolicyRepositories[TestId]);
            ReplaceProgramWithTestRepository<BookingRepository>(services, TestBookingRepositories[TestId]);

        
    });


    }

    private void ReplaceProgramWithTestRepository<TService>(IServiceCollection services, object implementation)
    {
        var descriptor = services.SingleOrDefault(
            d => d.ServiceType ==
                typeof(TService));
        if (descriptor != null) services.Remove(descriptor);
        services.AddSingleton(typeof(TService), implementation);
    }

    public InMemoryHotelRepository GetHotelRepository() => TestHotelRepositories[TestId];
    public InMemoryCompanyRepository GetCompanyRepository() => TestCompanyRepositories[TestId];
    public InMemoryEmployeeRepository GetEmployeeRepository() => TestEmployeeRepositories[TestId];
    public InMemoryBookingPolicyRepository GetBookingPolicyRepository() => TestBookingPolicyRepositories[TestId];
    public InMemoryBookingRepository GetBookingRepository() => TestBookingRepositories[TestId];

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
