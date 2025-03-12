using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
namespace HotelBookingKata;

public partial class Program {

    public static void Main(string[] args){
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        builder.Services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
        builder.Services.AddSingleton<CompanyRepository, InMemoryCompanyRepository>();
        builder.Services.AddSingleton<EmployeeRepository, InMemoryEmployeeRepository>();

        builder.Services.AddScoped<HotelService, ApplicationHotelService> ();
        builder.Services.AddScoped<CompanyService, ApplicationCompanyService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}