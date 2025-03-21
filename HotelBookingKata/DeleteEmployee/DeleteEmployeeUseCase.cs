using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.DeleteEmployee
{
    public class DeleteEmployeeUseCase
    {

        private CompanyRepository companyRepository;
        private EmployeeRepository employeeRepository;
        private BookingPolicyRepository bookingPolicyRepository;
        private BookingRepository bookingRepository;
        public DeleteEmployeeUseCase(CompanyRepository companyRepository, EmployeeRepository employeeRepository, BookingPolicyRepository bookingPolicyRepository, BookingRepository bookingRepository)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.bookingPolicyRepository = bookingPolicyRepository;
            this.bookingRepository = bookingRepository;
        }
        public DeleteEmployeeUseCase()
        {
        }

        public virtual void Execute(string employeeId)
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
            bookingPolicyRepository.DeleteEmployeePolicy(employeeId);
            bookingRepository.DeleteEmployeeBookings(employeeId);
            employeeRepository.Delete(employeeId);
        }
    }

}
