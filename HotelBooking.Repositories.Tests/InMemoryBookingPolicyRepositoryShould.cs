using HotelBookingKata.Entities;
using HotelBookingKata.Repositories;
using Shouldly;

namespace HotelBooking.Repositories.Tests;
class InMemoryBookingPolicyRepositoryShould
{
    private InMemoryBookingPolicyRepository repository;

    [SetUp]
    public void Setup()
    {
        repository = new InMemoryBookingPolicyRepository();
    }

    [Test]
    public void set_company_policy()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };

        repository.SetCompanyPolicy(companyId, roomTypes);

        repository.HasCompanyPolicy(companyId).ShouldBeTrue();  
        repository.IsRoomTypeAllowedForCompany(companyId, RoomType.Standard).ShouldBeTrue();
        repository.IsRoomTypeAllowedForCompany(companyId, RoomType.JuniorSuite).ShouldBeFalse();
    }
}

