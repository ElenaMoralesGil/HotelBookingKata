
using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using Shouldly;
namespace HotelBooking.Repositories.Tests;
class InMemoryEmployeeRepositoryShould
{

    private InMemoryEmployeeRepository repository;

    public InMemoryEmployeeRepositoryShould()
    {
        repository = new InMemoryEmployeeRepository();
    }

    [Test]
    public void add_employee_to_existing_employees()
    {
        var companyId = "Company1";
        var employee = new Employee("employee1", companyId);
        repository.Add(employee);
        var exists = repository.Exists(employee.Id);
        exists.ShouldBeTrue();
        var employees = repository.GetEmployees();
        employees.Count.ShouldBe(1);
        employees.ShouldContainKey("employee1");
    }
    [Test]
    public void get_id_from_existing_employee()
    {
        var companyId = "Company1";
        var employee = new Employee("employee2", companyId);
        repository.Add(employee);
        var result = repository.GetById(employee.Id);
        result.ShouldBe(employee);
    }

    [Test]
    public void return_true_when_employee_exists()
    {
        var companyId = "Company1";
        var employee = new Employee("employee3", companyId);
        repository.Add(employee);
        var result = repository.Exists(employee.Id);
        result.ShouldBeTrue();
    }

    [Test]
    public void return_false_when_employee_doesnt_exist()
    {
        var companyId = "Company1";
        var employee = new Employee("employee4", companyId);
        var result = repository.Exists(employee.Id);
        result.ShouldBeFalse();
    }

    [Test]
    public void delete_employee_when_employee_exists()
    {
        var companyId = "Company1";
        var employee = new Employee("employee6", companyId);
        repository.Add(employee);

        repository.Delete(employee.Id);
        var result = repository.Exists(employee.Id);

        result.ShouldBeFalse();
    }

}

