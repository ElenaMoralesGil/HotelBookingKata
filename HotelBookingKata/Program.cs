using HotelBookingKata.Common;
using HotelBookingKata.Repositories;
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

        builder.Services.AddScoped<HotelService, CompanyHotelService>();
        builder.Services.AddScoped<CompanyService, CompanyCompanyService>();
        builder.Services.AddScoped<BookingPolicyService, CompanyBookingPolicyService>();
        builder.Services.AddScoped<BookingService, CompanyBookingService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}