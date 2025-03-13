using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories;

public interface BookingRepository
{
    void Add(Booking booking);
    int CountBookingsByHotelRoomType(string hotelId, RoomType roomType, DateTime date);
    Dictionary<string, Booking> GetBookings();
    bool Exists(string id);

}
