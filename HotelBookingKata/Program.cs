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


        builder.Services.AddScoped<HotelService, ApplicationHotelService>();
        builder.Services.AddScoped<CompanyService, ApplicationCompanyService>();
        builder.Services.AddScoped<BookingPolicyService, ApplicationBookingPolicyService>();
        builder.Services.AddScoped<BookingService, ApplicationBookingService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}