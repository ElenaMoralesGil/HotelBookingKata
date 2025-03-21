using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
namespace HotelBookingKata.Services;

public class AppHotelService : HotelService
{

    private readonly HotelRepository HotelRepository;

    public AppHotelService(HotelRepository hotelRepository)
    {
        HotelRepository = hotelRepository;
    }

    public Hotel FindHotelBy(string hotelId)
    {
        return HotelRepository.GetById(hotelId);
    }
}