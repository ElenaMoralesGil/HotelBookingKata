using HotelBookingKata.Entities;
namespace HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;

public class SetEmployeePolicyRequest
{
    public string EmployeeId { get; set; }
    public List<RoomType> RoomTypes { get; set; }
}
