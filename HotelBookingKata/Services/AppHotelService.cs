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

    public void SetRoom(string hotelId, string roomNumber, RoomType roomType)
    {
        if (!HotelRepository.Exists(hotelId)) throw new HotelNotFoundException(hotelId);

        var hotel = HotelRepository.GetById(hotelId);
        hotel.SetRoom(roomNumber, roomType);
        HotelRepository.Update(hotel);
    }

    public Hotel FindHotelBy(string hotelId)
    {
        return HotelRepository.GetById(hotelId);
    }
}