using HotelBookingKata.Entities;
namespace HotelBookingKata.Services;

public interface BookingService
{
    Booking Book(string employeeId, string hotelId, RoomType roomType, DateTime checkIn, DateTime checkOut);
}
