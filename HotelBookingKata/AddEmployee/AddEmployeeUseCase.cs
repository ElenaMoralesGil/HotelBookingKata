using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.AddEmployee;

public class AddEmployeeUseCase
{
    private EmployeeRepository employeeRepository;
    private CompanyRepository companyRepository;

    public AddEmployeeUseCase(EmployeeRepository employeeRepository, CompanyRepository companyRepository)
    {
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
    }

    public AddEmployeeUseCase()
    {
    }
    public virtual void Execute(string companyId, AddEmployeeRequest request)
    {
        if (employeeRepository.Exists(request.EmployeeId)) throw new EmployeeAlreadyExistsException(request.EmployeeId);

        if (!companyRepository.Exists(companyId))
        {
            var company = new Company(companyId);
            companyRepository.Add(company);
        }

        var existingCompany = companyRepository.GetById(companyId);

        var employee = new Employee(request.EmployeeId, companyId);
        existingCompany.AddEmployee(employee);
        employeeRepository.Add(employee);

        companyRepository.Update(existingCompany);


    }
}
