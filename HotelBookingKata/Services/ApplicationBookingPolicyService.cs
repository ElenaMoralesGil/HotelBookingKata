using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.Services;
public class ApplicationBookingPolicyService : BookingPolicyService
{
    private BookingPolicyRepository bookingPolicyRepository;
    private EmployeeRepository employeeRepository;
    private CompanyRepository companyRepository;

    public ApplicationBookingPolicyService(
        BookingPolicyRepository bookingPolicyRepository, 
        EmployeeRepository employeeRepository, 
        CompanyRepository companyRepository)
    {
        this.bookingPolicyRepository = bookingPolicyRepository;
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
    }

    public bool IsBookingAllowed(string companyId, string employeeId, RoomType roomType)
    {
        if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        var employee = employeeRepository.GetById(employeeId);

        if (bookingPolicyRepository.HasEmployeePolicy(employeeId))
        {
            return bookingPolicyRepository.IsRoomTypeAlloedForEmployee(employeeId, roomType);
        }

        if (bookingPolicyRepository.HasCompanyPolicy(companyId))
        {
            return bookingPolicyRepository.IsRoomTypeAlloedForCompany(companyId, roomType);
        }

        return true;
    }

    public void SetCompanyPolicy(string companyId, List<RoomType> roomTypes)
    {
        if(!companyRepository.Exists(companyId)) throw new CompanyNotFoundException(companyId);

        bookingPolicyRepository.SetCompanyPolicy(companyId, roomTypes);
    }

    public void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes)
    {
        if(!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        bookingPolicyRepository.SetEmployeePolicy(employeeId, roomTypes);
    }
}

