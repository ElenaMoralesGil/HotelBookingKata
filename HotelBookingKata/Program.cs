using HotelBookingKata.Adapters;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using HotelBookingKata.CreateBooking;
using HotelBookingKata.AddHotel;
using HotelBookingKata.SetCompanyPolicy;
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

        builder.Services.AddScoped<CheckBookingPermissionRepository, CheckBookingPermissionAdapter>();

        builder.Services.AddScoped<CreateBookingUseCase>();
        builder.Services.AddScoped<AddHotelUseCase>();
        builder.Services.AddScoped<SetCompanyPolicyUseCase>();

        builder.Services.AddScoped<HotelService, AppHotelService>();
        builder.Services.AddScoped<CompanyService, AppCompanyService>();
        builder.Services.AddScoped<BookingPolicyService, AppBookingPolicyService>();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}