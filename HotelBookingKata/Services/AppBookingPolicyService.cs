using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.Services;
public class AppBookingPolicyService : BookingPolicyService
{
    private BookingPolicyRepository bookingPolicyRepository;
    private EmployeeRepository employeeRepository;

    public AppBookingPolicyService(
        BookingPolicyRepository bookingPolicyRepository,
        EmployeeRepository employeeRepository)
    {
        this.bookingPolicyRepository = bookingPolicyRepository;
        this.employeeRepository = employeeRepository;
    }

    public bool IsBookingAllowed(string employeeId, RoomType roomType)
    {
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
}

