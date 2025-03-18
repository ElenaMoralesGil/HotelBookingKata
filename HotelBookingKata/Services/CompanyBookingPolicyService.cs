using HotelBookingKata.Common;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;
namespace HotelBookingKata.Services;
public class CompanyBookingPolicyService : BookingPolicyService
{
    private BookingPolicyRepository bookingPolicyRepository;
    private Dispatcher dispatcher;
    private EmployeeRepository employeeRepository;
    private CompanyRepository companyRepository;

    public CompanyBookingPolicyService(
        BookingPolicyRepository bookingPolicyRepository,
        EmployeeRepository employeeRepository,
        CompanyRepository companyRepository,
        Dispatcher dispatcher =  null )
    {
        this.bookingPolicyRepository = bookingPolicyRepository;
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.dispatcher = dispatcher;
 
    }

    public bool IsBookingAllowed(string employeeId, RoomType roomType) {

        if (dispatcher != null)
        {

            return dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(
                new CheckBookingPolicyRequest
                {
                    EmployeeId = employeeId,
                    RoomType = roomType
                });
        }

        if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        var employee = employeeRepository.GetById(employeeId);

        if (bookingPolicyRepository.HasEmployeePolicy(employeeId))
        {
            return bookingPolicyRepository.IsRoomTypeAllowedForEmployee(employeeId, roomType);
        }

        if (bookingPolicyRepository.HasCompanyPolicy(employee.CompanyId))
        {
            return bookingPolicyRepository.IsRoomTypeAllowedForCompany(employee.CompanyId, roomType);
        }

        return true;
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
        
        //if (!companyRepository.Exists(companyId)) throw new CompanyNotFoundException(companyId);

        //bookingPolicyRepository.SetCompanyPolicy(companyId, roomTypes);


    }


    public void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes)
    {
        if(dispatcher != null)
        {
            dispatcher.Dispatch<SetEmployeePolicyRequest>(new SetEmployeePolicyRequest
            {
                EmployeeId = employeeId,
                RoomTypes = roomTypes
            });
        }
        //if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        //bookingPolicyRepository.SetEmployeePolicy(employeeId, roomTypes);
    }
}

