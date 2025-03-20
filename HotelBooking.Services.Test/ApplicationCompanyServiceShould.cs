
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Services.Test;
class ApplicationCompanyServiceShould
{
    private CompanyRepository companyRepository;
    private EmployeeRepository employeeRepository;
    private BookingRepository bookingRepository;
    private BookingPolicyRepository bookingPolicyRepository;
    private AppCompanyService companyService;

    [SetUp]
    public void Setup()
    {
        companyRepository = Substitute.For<CompanyRepository>();
        employeeRepository = Substitute.For<EmployeeRepository>();
        bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
        bookingRepository = Substitute.For<BookingRepository>();
        companyService = new AppCompanyService(companyRepository, employeeRepository, bookingPolicyRepository, bookingRepository);
    }

    [Test]
    public void delete_employee_when_employee_exists_with_all_their_bookings_and_policies()
    {
        var employeeId = "Employee1";
        var employee = new Employee(employeeId, "Company1");
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        companyRepository.Exists(employee.CompanyId).Returns(true);
        companyRepository.GetById(employee.CompanyId).Returns(new Company(employee.CompanyId));

        companyService.DeleteEmployee(employeeId);

        employeeRepository.Received(1).Delete(employeeId);
        bookingRepository.Received(1).DeleteEmployeeBookings(employeeId);
        bookingPolicyRepository.Received(1).DeleteEmployeePolicy(employeeId);
        companyRepository.Received(1).Update(Arg.Is<Company>(c => c.Id == employee.CompanyId));
    }
}

