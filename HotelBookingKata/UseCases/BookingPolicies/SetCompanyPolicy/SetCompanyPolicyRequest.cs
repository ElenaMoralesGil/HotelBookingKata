using HotelBookingKata.Entities;
namespace HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;

public class SetCompanyPolicyRequest
{
    public String CompanyId { get; set; }
    public List<RoomType> RoomTypes { get; set; }
}
