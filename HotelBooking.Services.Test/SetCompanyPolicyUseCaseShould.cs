using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Repositories;
using HotelBookingKata.Services;
using HotelBookingKata.SetCompanyPolicy;
using NSubstitute;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelBooking.Services.Test;

class SetCompanyPolicyUseCaseShould
{
    private BookingPolicyRepository bookingPolicyRepository;
    private CompanyRepository companyRepository;
    private SetCompanyPolicyUseCase  useCase;

    [SetUp]
    public void Setup()
    {
        bookingPolicyRepository = Substitute.For<BookingPolicyRepository>();
        companyRepository = Substitute.For<CompanyRepository>();
        useCase = new SetCompanyPolicyUseCase(companyRepository, bookingPolicyRepository);
    }


    [Test]
    public void set_company_policy_when_company_exists()
    {

        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetCompanyPolicyRequest(roomTypes);
        companyRepository.Exists(companyId).Returns(true);

        companyRepository.GetById(companyId).Returns(new Company(companyId));
        useCase.Execute(companyId, request);

        bookingPolicyRepository.Received(1).SetCompanyPolicy(companyId, roomTypes);
    }

    [Test]
    public void throw_exception_when_company_does_not_exist()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetCompanyPolicyRequest(roomTypes);
        companyRepository.Exists(companyId).Returns(false);

        Should.Throw<CompanyNotFoundException>(() => useCase.Execute(companyId, request));
    }
}
