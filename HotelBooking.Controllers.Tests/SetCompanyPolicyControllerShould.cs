using HotelBookingKata.Entities;
using HotelBookingKata.SetCompanyPolicy;
using Microsoft.AspNetCore.Mvc;
using Shouldly;
using NSubstitute;
using HotelBookingKata.Exceptions;

namespace HotelBooking.Controllers.Tests;

class SetCompanyPolicyControllerShould
{
    private SetCompanyPoliciyController controller;
    private SetCompanyPolicyUseCase useCase;

    [SetUp]
    public void Setup()
    {
        useCase = Substitute.For<SetCompanyPolicyUseCase>();
        controller = new SetCompanyPoliciyController(useCase);
    }
    [Test]
    public void return_ok_when_setting_valid_company_policy()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var request = new SetCompanyPolicyRequest(roomTypes);

        var result = controller.SetCompanyPolicy(companyId, request);

        result.ShouldBeOfType<OkResult>();
        useCase.Received(1).Execute(companyId,request);
    }
    [Test]
    public void returns_not_found_when_setting_policy_for_non_existent_company()
    {
        var companyId = "Company1";
        var request = new SetCompanyPolicyRequest(new List<RoomType> { RoomType.Standard });
        useCase.When(x => x.Execute(companyId, request)).Throw(new CompanyNotFoundException(companyId));

        var result = controller.SetCompanyPolicy(companyId, request);

        result.ShouldBeOfType<NotFoundObjectResult>();
        useCase.Received(1).Execute(companyId, request);
    }

}
