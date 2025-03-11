using HotelBookingKata.Repositories;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.services;

public class HotelServiceImpl : HotelService{

    private readonly HotelRepository HotelRepository;

    public HotelServiceImpl(HotelRepository hotelRepository){
        HotelRepository= hotelRepository;
    }

    public void AddHotel(string hotelId, string hotelName){

        if (HotelRepository.Exists(hotelId)) throw new HotelAlreadyExistsException(hotelId);
       
        var hotel =  new Hotel(hotelId,hotelName);
        HotelRepository.Add(hotel);
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