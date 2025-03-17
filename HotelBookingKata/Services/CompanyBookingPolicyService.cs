using HotelBookingKata.Common;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using HotelBookingKata.UseCases.BookingPolicies.CheckBookingPolicy;
public class CompanyBookingPolicyService : BookingPolicyService
{
    private BookingPolicyRepository bookingPolicyRepository;
    private Dispatcher dispatcher;
    private EmployeeRepository employeeRepository;
    private CompanyRepository companyRepository;

    public CompanyBookingPolicyService(
        BookingPolicyRepository bookingPolicyRepository,
        EmployeeRepository employeeRepository,
        CompanyRepository companyRepository,
        Dispatcher dispatcher )
    {
        this.bookingPolicyRepository = bookingPolicyRepository;
        this.employeeRepository = employeeRepository;
        this.companyRepository = companyRepository;
        this.dispatcher = dispatcher;
    }

    public bool IsBookingAllowed(string employeeId, RoomType roomType) {
        return dispatcher.Dispatch<CheckBookingPolicyRequest, bool>(
            new CheckBookingPolicyRequest { 
                EmployeeId = employeeId, 
                RoomType = roomType 
            });
    }

    //public bool IsBookingAllowed(string employeeId, RoomType roomType)
    //{
    //    if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

    //    var employee = employeeRepository.GetById(employeeId);

    //    if (bookingPolicyRepository.HasEmployeePolicy(employeeId))
    //    {
    //        return bookingPolicyRepository.IsRoomTypeAllowedForEmployee(employeeId, roomType);
    //    }

    //    if (bookingPolicyRepository.HasCompanyPolicy(employee.CompanyId))
    //    {
    //        return bookingPolicyRepository.IsRoomTypeAllowedForCompany(employee.CompanyId, roomType);
    //    }

    //    return true;
    //}

    public void SetCompanyPolicy(string companyId, List<RoomType> roomTypes)
    {
        if (!companyRepository.Exists(companyId)) throw new CompanyNotFoundException(companyId);

        bookingPolicyRepository.SetCompanyPolicy(companyId, roomTypes);
    }

    public void SetEmployeePolicy(string employeeId, List<RoomType> roomTypes)
    {
        if (!employeeRepository.Exists(employeeId)) throw new EmployeeNotFoundException(employeeId);

        bookingPolicyRepository.SetEmployeePolicy(employeeId, roomTypes);
    }
}

