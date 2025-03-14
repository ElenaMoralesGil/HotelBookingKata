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
    private CompanyRepository companyRepository;
    private ApplicationBookingPolicyService bookingPolicyService;

    [SetUp]
    public void Setup()
    {
        bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
        employeeRepository = Substitute.For<EmployeeRepository>();
        companyRepository = Substitute.For<CompanyRepository>();
        bookingPolicyService = new ApplicationBookingPolicyService(bookingPolicyRepository, employeeRepository, companyRepository);
    }

    [Test]
    public void set_company_policy_when_company_exists()
    {

        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        companyRepository.Exists(companyId).Returns(true);

        companyRepository.GetById(companyId).Returns(new Company(companyId));
        bookingPolicyService.SetCompanyPolicy(companyId, roomTypes);

        bookingPolicyRepository.Received(1).SetCompanyPolicy(companyId, roomTypes);
    }

    [Test]

    public void throw_exception_when_company_does_not_exist()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        companyRepository.Exists(companyId).Returns(false);

        Should.Throw<CompanyNotFoundException>(() => bookingPolicyService.SetCompanyPolicy(companyId, roomTypes));
    }

    [Test]
    public void set_employee_policy_when_employee_exists()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        employeeRepository.Exists(employeeId).Returns(true);

        employeeRepository.GetById(employeeId).Returns(new Employee(employeeId, "Company1"));
        bookingPolicyService.SetEmployeePolicy(employeeId, roomTypes);

        bookingPolicyRepository.Received(1).SetEmployeePolicy(employeeId, roomTypes);
    }

    [Test]
    public void throw_exception_when_employee_does_not_exist()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        employeeRepository.Exists(employeeId).Returns(false);

        Should.Throw<EmployeeNotFoundException>(() => bookingPolicyService.SetEmployeePolicy(employeeId, roomTypes));
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
