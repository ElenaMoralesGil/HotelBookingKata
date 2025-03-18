using HotelBookingKata.Common;
using HotelBookingKata.Entities;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;
namespace HotelBookingKata.Services;
public class AppBookingPolicyService : BookingPolicyService
{
    private Dispatcher dispatcher;

    public AppBookingPolicyService(
        Dispatcher dispatcher = null)
    {
        this.dispatcher = dispatcher;

    }

    public bool IsBookingAllowed(string employeeId, RoomType roomType)
    {


        return dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(
            new CheckBookingPolicyRequest
            {
                EmployeeId = employeeId,
                RoomType = roomType
            });
    }


    public void SetCompanyPolicy(string companyId, List<RoomType> roomTypes)
    {

        if (dispatcher != null)
        {
            dispatcher.Dispatch<SetCompanyPolicyRequest>(new SetCompanyPolicyRequest
            {
                CompanyId = companyId,
                RoomTypes = roomTypes
            });
        }
    }


    public void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes)
    {
        if (dispatcher != null)
        {
            dispatcher.Dispatch<SetEmployeePolicyRequest>(new SetEmployeePolicyRequest
            {
                EmployeeId = employeeId,
                RoomTypes = roomTypes
            });
        }
    }
}

