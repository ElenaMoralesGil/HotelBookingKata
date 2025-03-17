using HotelBookingKata.Common;
using HotelBookingKata.Repositories;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy
{
    public class CheckBookingPolicyHandler : UseCase<CheckBookingPolicyRequest, bool>
    {
        private BookingPolicyRepository bookingPolicyRepository;
        private EmployeeRepository employeeRepository;

        public CheckBookingPolicyHandler(BookingPolicyRepository bookingPolicyRepository, EmployeeRepository employeeRepository)
        {
            this.bookingPolicyRepository = bookingPolicyRepository;
            this.employeeRepository = employeeRepository;
        }

        public bool Execute(CheckBookingPolicyRequest request)
        {
            if (!employeeRepository.Exists(request.EmployeeId)) throw new EmployeeNotFoundException(request.EmployeeId);

            var employee = employeeRepository.GetById(request.EmployeeId);

            if (bookingPolicyRepository.HasEmployeePolicy(request.EmployeeId))
            {
                return bookingPolicyRepository.IsRoomTypeAllowedForEmployee(request.EmployeeId, request.RoomType);
            }

            if (bookingPolicyRepository.HasCompanyPolicy(employee.CompanyId))
            {
                return bookingPolicyRepository.IsRoomTypeAllowedForCompany(employee.CompanyId, request.RoomType);
            }
            return true;
        }
    }
}
