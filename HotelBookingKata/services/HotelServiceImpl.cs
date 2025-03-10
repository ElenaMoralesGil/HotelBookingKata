using HotelBookingKata.Repositories;
using HotelBookingKata.Entities;
namespace HotelBookingKata.services;

public class HotelServiceImpl : HotelService{

    private readonly HotelRepository HotelRepository;

    public HotelServiceImpl(HotelRepository hotelRepository){
        HotelRepository= hotelRepository;
    }

    public void AddHotel(string hotelId, string hotelName){

        if (HotelRepository.Exists(hotelId)) throw new InvalidOperationException("Hotel already exists");
       
        var hotel =  new Hotel(hotelId,hotelName);
        HotelRepository.Add(hotel);
    }

    public void SetRoom(string hotelId, string roomNumber, RoomType roomType)
    {
        var hotel = HotelRepository.GetById(hotelId);
        if (hotel == null) throw new InvalidOperationException("Hotel does not exist");
        hotel.SetRoom(roomNumber, roomType);
        HotelRepository.Update(hotel);
    }
}