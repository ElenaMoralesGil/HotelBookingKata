using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.SetEmployeePolicy;

public class SetEmployeePolicyUseCase
{
    private EmployeeRepository employeeRepository;
    private BookingPolicyRepository bookingPolicyRepository;

    public SetEmployeePolicyUseCase(EmployeeRepository employeeRepository, BookingPolicyRepository bookingPolicyRepository)
    {
        this.employeeRepository = employeeRepository;
        this.bookingPolicyRepository = bookingPolicyRepository;
    }

    public SetEmployeePolicyUseCase() { }

    public virtual void Execute(string employeeId, SetEmployeePolicyRequest request)
    {
        if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        bookingPolicyRepository.SetEmployeePolicy(employeeId, request.RoomTypes);
    }
}
