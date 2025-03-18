using HotelBookingKata.Common;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;
using HotelBookingKata.UseCases.BookingPolicies.SetEmployeePolicy;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;
namespace HotelBooking.Services.Test;
class ApplicationBookingPolicyServiceShould
{
    private CompanyBookingPolicyService bookingPolicyService;
    private Dispatcher dispatcher;

    [SetUp]
    public void Setup()
    {
        dispatcher = Substitute.For<Dispatcher>();
        bookingPolicyService = new CompanyBookingPolicyService(dispatcher);
    }

    [Test]
    public void set_company_policy_when_company_exists()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        bookingPolicyService.SetCompanyPolicy(companyId, roomTypes);
        dispatcher.Received(1).Dispatch(Arg.Is<SetCompanyPolicyRequest>(request =>
      request.CompanyId == companyId && request.RoomTypes.SequenceEqual(roomTypes)));

        dispatcher.Received(1).Dispatch(Arg.Is<SetCompanyPolicyRequest>(request => request.CompanyId == companyId && request.RoomTypes.SequenceEqual(roomTypes)));
    }


    [Test]
    public void throw_exception_when_company_does_not_exist()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        dispatcher
            .When(d => d.Dispatch(Arg.Any<SetCompanyPolicyRequest>()))
            .Do(call => throw new CompanyNotFoundException(companyId));
        Should.Throw<CompanyNotFoundException>(() => bookingPolicyService.SetCompanyPolicy(companyId, roomTypes));
    }


    [Test]
    public void set_employee_policy_when_employee_exists()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        bookingPolicyService.SetEmployeePolicy(employeeId, roomTypes);

        dispatcher.Received(1).Dispatch(Arg.Is<SetEmployeePolicyRequest>(request =>
            request.EmployeeId == employeeId && request.RoomTypes.SequenceEqual(roomTypes)));
    }

    [Test]
    public void throw_exception_when_employee_does_not_exist()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        dispatcher
            .When(d => d.Dispatch(Arg.Any<SetEmployeePolicyRequest>()))
            .Do(call => throw new EmployeeNotFoundException(employeeId));
        Should.Throw<EmployeeNotFoundException>(() => bookingPolicyService.SetEmployeePolicy(employeeId, roomTypes));
    }

    [Test]
    public void return_true_when_booking_is_allowed_by_employee_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;

        var employee = new Employee(employeeId, companyId);
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(
              Arg.Any<CheckBookingPolicyRequest>()).Returns(true);
        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeTrue();
    }

    [Test]
    public void return_true_when_booking_is_allowed_by_company_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        var employee = new Employee(employeeId, companyId);
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(Arg.Any<CheckBookingPolicyRequest>()).Returns(true);

        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeTrue();
    }

    [Test]
    public void return_true_when_there_are_no_policies()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(Arg.Any<CheckBookingPolicyRequest>()).Returns(true);
        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeTrue();
    }

    [Test]
    public void return_false_when_booking_is_not_allowed_by_employee_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        var employee = new Employee(employeeId, companyId);
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(Arg.Any<CheckBookingPolicyRequest>()).Returns(false);
        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeFalse();
    }

    [Test]
    public void return_false_when_booking_is_not_allowed_by_company_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        var employee = new Employee(employeeId, companyId);
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(Arg.Any<CheckBookingPolicyRequest>()).Returns(false);

        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeFalse();
    }

    [Test]
    public void priorize_employee_policy_over_company_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        var employee = new Employee(employeeId, companyId);
        dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(Arg.Any<CheckBookingPolicyRequest>()).Returns(false);
        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);
        isAllowed.ShouldBeFalse();
    }
}
