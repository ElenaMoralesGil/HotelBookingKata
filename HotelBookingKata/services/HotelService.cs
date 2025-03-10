using HotelBookingKata.Entities;
namespace HotelBookingKata.services
{
    public interface HotelService
    {
        void AddHotel(string hotelId, string hotelName);
        void SetRoom(string hotelId, string roomNumber, RoomType roomType);

    }
}
