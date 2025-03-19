using HotelBookingKata.Entities;
namespace HotelBookingKata.Adapters;

public interface BookingPolicyAdapter
{
    Task<bool> IsBookingAllowed(string employeeId, RoomType roomType);
}
