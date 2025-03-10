using HotelBookingKata.Repositories;
using HotelBookingKata.services;
namespace HotelBookingKata;

public partial class Program {

    public static void Main(string[] args){
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddSingleton<HotelRepository, InMemoryHotelRepository>();
        builder.Services.AddScoped<HotelService, HotelServiceImpl> ();

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}