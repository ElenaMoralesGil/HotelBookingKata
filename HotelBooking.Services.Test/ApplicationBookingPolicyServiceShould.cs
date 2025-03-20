using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Services.Test;
class ApplicationBookingPolicyServiceShould
{
    private BookingPolicyRepository bookingPolicyRepository;
    private EmployeeRepository employeeRepository;
    private AppBookingPolicyService bookingPolicyService;

    [SetUp]
    public void Setup()
    {
        bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
        employeeRepository = Substitute.For<EmployeeRepository>();
        bookingPolicyService = new AppBookingPolicyService(bookingPolicyRepository, employeeRepository);
    }




    [Test]
    public void return_true_when_booking_is_allowed_by_employee_policy()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;

        var employee = new Employee(employeeId, companyId);
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForEmployee(employeeId, roomType).Returns(true);

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
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(false);
        bookingPolicyRepository.HasCompanyPolicy(companyId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForCompany(companyId, roomType).Returns(true);

        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);

        isAllowed.ShouldBeTrue();
    }

    [Test]
    public void return_true_when_there_are_no_policies()
    {
        var employeeId = "Employee1";
        var companyId = "Company1";
        var roomType = RoomType.Standard;
        var employee = new Employee(employeeId, companyId);
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(false);
        bookingPolicyRepository.HasCompanyPolicy(companyId).Returns(false);

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
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForEmployee(employeeId, roomType).Returns(false);

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
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(false);
        bookingPolicyRepository.HasCompanyPolicy(companyId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForCompany(companyId, roomType).Returns(false);

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
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        bookingPolicyRepository.HasEmployeePolicy(employeeId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForEmployee(employeeId, roomType).Returns(false);
        bookingPolicyRepository.HasCompanyPolicy(companyId).Returns(true);
        bookingPolicyRepository.IsRoomTypeAllowedForCompany(companyId, roomType).Returns(true);

        var isAllowed = bookingPolicyService.IsBookingAllowed(employeeId, roomType);
        isAllowed.ShouldBeFalse();
    }
}
