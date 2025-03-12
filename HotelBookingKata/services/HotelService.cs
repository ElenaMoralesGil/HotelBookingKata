using HotelBookingKata.Entities;
namespace HotelBookingKata.Services
{
    public interface HotelService
    {
        void AddHotel(string hotelId, string hotelName);
        void SetRoom(string hotelId, string roomNumber, RoomType roomType);
        Hotel FindHotelBy(string hotelId);

    }
}
