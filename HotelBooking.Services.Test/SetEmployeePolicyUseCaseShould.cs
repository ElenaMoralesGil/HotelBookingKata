using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.SetEmployeePolicy;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Services.Test;

class SetEmployeePolicyUseCaseShould
{
    private BookingPolicyRepository bookingPolicyRepository;
    private EmployeeRepository employeeRepository;
    private SetEmployeePolicyUseCase useCase;

    [SetUp]
    public void Setup()
    {
        bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
        employeeRepository = Substitute.For<EmployeeRepository>();
        useCase = new SetEmployeePolicyUseCase(employeeRepository, bookingPolicyRepository);
    }

    [Test]
    public void set_employee_policy_when_employee_exists()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetEmployeePolicyRequest( roomTypes);
        employeeRepository.Exists(employeeId).Returns(true);

        employeeRepository.GetById(employeeId).Returns(new Employee(employeeId, "Company1"));
        useCase.Execute(employeeId, request);

        bookingPolicyRepository.Received(1).SetEmployeePolicy(employeeId, roomTypes);
    }

    [Test]
    public void throw_exception_when_employee_does_not_exist()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetEmployeePolicyRequest(roomTypes);
        employeeRepository.Exists(employeeId).Returns(false);

        Should.Throw<EmployeeNotFoundException>(() => useCase.Execute(employeeId, request));
    }
}
