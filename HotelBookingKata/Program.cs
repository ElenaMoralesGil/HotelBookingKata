using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using HotelBookingKata.SetCompanyPolicy;
using HotelBookingKata.SetEmployeePolicy;
using HotelBookingKata.CheckBookingPolicy;
using HotelBookingKata.AddHotel;
using HotelBookingKata.AddEmployee;
using HotelBookingKata.CreateBooking;
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

        builder.Services.AddScoped<CheckBookingPolicyRepository, CheckBookingPolicyAdapter>();

        builder.Services.AddScoped<CreateBookingUseCase>();
        builder.Services.AddScoped<SetCompanyPolicyUseCase>();
        builder.Services.AddScoped<SetEmployeePolicyUseCase>();
        builder.Services.AddScoped<CheckBookingPolicyUseCase>();
        builder.Services.AddScoped<AddHotelUseCase>();
        builder.Services.AddScoped<AddEmployeeUseCase>();
        builder.Services.AddScoped<HotelService, AppHotelService>();
        builder.Services.AddScoped<CompanyService, AppCompanyService>();
   

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}