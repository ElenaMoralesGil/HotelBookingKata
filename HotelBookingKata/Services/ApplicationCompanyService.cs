using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
namespace HotelBookingKata.Services;
public class ApplicationCompanyService : CompanyService
{

    private CompanyRepository companyRepository;
    private EmployeeRepository employeeRepository;
    public ApplicationCompanyService(CompanyRepository companyRepository, EmployeeRepository employeeRepository)
    {
        this.companyRepository = companyRepository;
        this.employeeRepository = employeeRepository;
    }
    public void AddEmployee(string companyId, string employeeId)
    {
        if (employeeRepository.Exists(employeeId)) throw new EmployeeAlreadyExistsException(employeeId);

        if (!companyRepository.Exists(companyId))
        {
            var company = new Company(companyId);
            companyRepository.Add(company);
        }

        var existingCompany = companyRepository.GetById(companyId);

        var employee = new Employee(employeeId, companyId);
        existingCompany.AddEmployee(employee);
        employeeRepository.Add(employee);

        companyRepository.Update(existingCompany);


    }

    public void DeleteEmployee(string employeeId)
    {

        if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);
        var employee = employeeRepository.GetById(employeeId);
        var companyId = employee.CompanyId;

        if (companyRepository.Exists(companyId))
        {
            var company = companyRepository.GetById(companyId);
            company.RemoveEmployee(employee);
            companyRepository.Update(company);
        }
        employeeRepository.Delete(employeeId);
    }
}
