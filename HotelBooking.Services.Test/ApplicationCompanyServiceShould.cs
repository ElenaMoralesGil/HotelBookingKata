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
            companyRepository = Substitute.For<CompanyRepository>();
            employeeRepository = Substitute.For<EmployeeRepository>();
            companyService = new ApplicationCompanyService(companyRepository, employeeRepository);
        }

        [Test]

        public void add_employee_when_employee_doesnt_exist_in_company()
        {
        var companyId = "Company1";
        var employeeId = "Employee1";
        var company = new Company(companyId);

        companyRepository.Exists(companyId).Returns(true);
        companyRepository.GetById(companyId).Returns(company);
        employeeRepository.Exists(employeeId).Returns(false);

        companyService.AddEmployee(company.Id, employeeId);

        employeeRepository.Received(1).Add(Arg.Is<Employee>(e => e.Id == employeeId && e.CompanyId == companyId));
        companyRepository.Received(1).Update(company);
    }
    }

