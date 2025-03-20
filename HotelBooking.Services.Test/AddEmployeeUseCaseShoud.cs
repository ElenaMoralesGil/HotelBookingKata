using HotelBookingKata.AddEmployee;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using NSubstitute;
using Shouldly;

namespace HotelBooking.Services.Test;

class AddEmployeeUseCaseShoud
{
    private CompanyRepository companyRepository;
    private EmployeeRepository employeeRepository;
    private AddEmployeeUseCase useCase;

    [SetUp]
    public void Setup()
    {
        companyRepository = Substitute.For<CompanyRepository>();
        employeeRepository = Substitute.For<EmployeeRepository>();
        useCase = new AddEmployeeUseCase(employeeRepository, companyRepository);
    }

    [Test]

    public void add_employee_when_employee_doesnt_exist_in_company()
    {
        var companyId = "Company1";
        var employeeId = "Employee1";
        var company = new Company(companyId);
        var request = new AddEmployeeRequest(employeeId);
        companyRepository.Exists(companyId).Returns(true);
        companyRepository.GetById(companyId).Returns(company);
        employeeRepository.Exists(employeeId).Returns(false);
        useCase.Execute(company.Id, request);

        employeeRepository.Received(1).Add(Arg.Is<Employee>(e => e.Id == employeeId && e.CompanyId == companyId));
        companyRepository.Received(1).Update(company);
    }

    [Test]
    public void throw_exception_when_employee_already_exists_in_company()
    {
        var companyId = "Company1";
        var employeeId = "Employee1";
        var company = new Company(companyId);
        var employee = new Employee(employeeId, companyId);
        var request = new AddEmployeeRequest(employeeId);
        companyRepository.Exists(companyId).Returns(true);
        companyRepository.GetById(companyId).Returns(company);
        employeeRepository.Exists(employeeId).Returns(true);
        employeeRepository.GetById(employeeId).Returns(employee);
        Should.Throw<EmployeeAlreadyExistsException>(() => useCase.Execute(company.Id,request));
    }
}
