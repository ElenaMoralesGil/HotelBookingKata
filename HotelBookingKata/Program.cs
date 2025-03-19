using HotelBookingKata.Adapters;
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
        string apiURl = "https://localhost:5129";
        builder.Services.AddHttpClient<ApiBookingPolicyAdapter>(client => client.BaseAddress = new Uri(apiURl));
        builder.Services.AddScoped<BookingPolicyAdapter, ApiBookingPolicyAdapter>();
       
        builder.Services.AddScoped<HotelService, AppHotelService>();
        builder.Services.AddScoped<CompanyService,AppCompanyService>();
        builder.Services.AddScoped<BookingPolicyService, AppBookingPolicyService>();
        builder.Services.AddScoped<BookingService,AppBookingService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}