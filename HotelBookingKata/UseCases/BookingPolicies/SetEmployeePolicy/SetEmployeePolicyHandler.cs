using HotelBookingKata.Common;
using HotelBookingKata.Repositories;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;

public class SetEmployeePolicyHandler : UseCase<SetEmployeePolicyRequest>
{
    private BookingPolicyRepository bookingPolicyRepository;
    private EmployeeRepository employeeRepository;

    public SetEmployeePolicyHandler(BookingPolicyRepository bookingPolicyRepository, EmployeeRepository employeeRepository)
    {
        this.bookingPolicyRepository = bookingPolicyRepository;
        this.employeeRepository = employeeRepository;
    }
    public void Execute(SetEmployeePolicyRequest request)
    {
        if (!employeeRepository.Exists(request.EmployeeId)) throw new EmployeeNotFoundException(request.EmployeeId);
        bookingPolicyRepository.SetEmployeePolicy(request.EmployeeId, request.RoomTypes);
    }
}
