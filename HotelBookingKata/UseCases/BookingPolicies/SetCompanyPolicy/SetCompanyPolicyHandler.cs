using HotelBookingKata.Controllers;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Common;
using System.Runtime.CompilerServices;

namespace HotelBookingKata.UseCases.BookingPolicies.SetCompanyPolicy;

public class SetCompanyPolicyHandler : UseCase<SetCompanyPolicyRequest>
{
    private CompanyRepository companyRepository;
    private BookingPolicyRepository bookingPolicyRepository;
    public SetCompanyPolicyHandler(CompanyRepository companyRepository, BookingPolicyRepository bookingPolicyRepository)
    {
        this.companyRepository = companyRepository;
        this.bookingPolicyRepository = bookingPolicyRepository;
    }
    public void Execute(SetCompanyPolicyRequest request)
    {
        if (!companyRepository.Exists(request.CompanyId)) throw new CompanyNotFoundException(request.CompanyId);

        bookingPolicyRepository.SetCompanyPolicy(request.CompanyId, request.RoomTypes);
    }
}