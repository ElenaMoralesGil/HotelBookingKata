using HotelBookingKata.Common;
using HotelBookingKata.Repositories;
using HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
using HotelBookingKata.Services;
namespace HotelBookingKata;

public partial class Program
{

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
        builder.Services.AddSingleton<CompanyRepository, InMemoryCompanyRepository>();
        builder.Services.AddSingleton<EmployeeRepository, InMemoryEmployeeRepository>();
        builder.Services.AddSingleton<BookingPolicyRepository, InMemoryBookingPolicyRepository>();
        builder.Services.AddSingleton<BookingRepository, InMemoryBookingRepository>();

        builder.Services.AddScoped<Dispatcher, UseCaseDispatcher>();
        builder.Services.AddScoped<UseCase<SetCompanyPolicyRequest>, SetCompanyPolicyHandler>();
        builder.Services.AddScoped<UseCase<SetEmployeePolicyRequest>, SetEmployeePolicyHandler>();
        builder.Services.AddScoped<UseCase<CheckBookingPolicyRequest, bool>, CheckBookingPolicyHandler>();

        builder.Services.AddScoped<HotelService, AppHotelService>();
        builder.Services.AddScoped<CompanyService, AppCompanyService>();
        builder.Services.AddScoped<BookingPolicyService, AppBookingPolicyService>();
        builder.Services.AddScoped<BookingService, AppBookingService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}