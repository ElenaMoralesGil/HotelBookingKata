using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;

namespace HotelBookingKata.SetCompanyPolicy;

public class SetCompanyPolicyUseCase
{
    private CompanyRepository companyRepository;
    private BookingPolicyRepository bookingPolicyRepository;

    public SetCompanyPolicyUseCase() { }
    public SetCompanyPolicyUseCase(CompanyRepository companyRepository, BookingPolicyRepository bookingPolicyRepository)
    {
        this.companyRepository = companyRepository;
        this.bookingPolicyRepository = bookingPolicyRepository;
    }
    public virtual void Execute(string companyId, SetCompanyPolicyRequest request)
    {
        if (!companyRepository.Exists(companyId)) throw new CompanyNotFoundException(companyId);

        bookingPolicyRepository.SetCompanyPolicy(companyId, request.RoomTypes);
    }
}
