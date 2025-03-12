using System.Net;
using NSubstitute;
using Shouldly;
using HotelBookingKata.Services;
using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using HotelBookingKata.Exceptions;

namespace HotelBooking.Services.Test;
    class ApplicationCompanyServiceShould
    {
        private CompanyRepository companyRepository;
        private EmployeeRepository employeeRepository;
        private ApplicationCompanyService companyService;

        [SetUp]
        public void Setup()
        {
            companyRepository = Substitute.For<InMemoryCompanyRepository>();
            employeeRepository = Substitute.For<InMemoryEmployeeRepository>();
            companyService = new ApplicationCompanyService(companyRepository, employeeRepository);
        }

        [Test]

        public void add_employee_when_employee_doesnt_exist_in_company()
        {
        var company = new Company("Company1");
        var employee = new Employee("Employee1", company.Id);
        companyRepository.Exists(company.Id).Returns(true);
        employeeRepository.Exists(employee.Id).Returns(false);
        companyService.AddEmployee(company.Id, employee.Id);
        companyRepository.Received(1).Add(company);
        employeeRepository.Received(1).Add(employee);
    }
    }

