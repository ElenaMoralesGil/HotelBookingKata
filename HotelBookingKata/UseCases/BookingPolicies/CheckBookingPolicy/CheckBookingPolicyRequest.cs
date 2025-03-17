using HotelBookingKata.Entities;

namespace HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy
{
    public class CheckBookingPolicyRequest
    {
        public String EmployeeId { get; set; }
        public RoomType RoomType { get; set; }
    }
}
