using HotelBookingKata.Entities;
namespace HotelBookingKata.CheckBookingPolicy;

public interface CheckBookingPolicyRepository
{
    Task<bool> IsBookingAllowed(string employeeId, RoomType roomType);
}
