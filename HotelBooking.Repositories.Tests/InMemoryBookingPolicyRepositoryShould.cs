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

    [Test]
    public void update_company_policy()
    {
        var companyId = "Company1";
        var roomTypes = new List<RoomType> { RoomType.Standard };
        var updatedRoomTypes = new List<RoomType> { RoomType.JuniorSuite };

        repository.SetCompanyPolicy(companyId, roomTypes);
        repository.SetCompanyPolicy(companyId, updatedRoomTypes);

        repository.HasCompanyPolicy(companyId).ShouldBeTrue();
        repository.IsRoomTypeAllowedForCompany(companyId, RoomType.Standard).ShouldBeFalse();
        repository.IsRoomTypeAllowedForCompany(companyId, RoomType.JuniorSuite).ShouldBeTrue();
    }

    [Test]
    public void set_employee_policy()
    {
        var employeeId = "Employee1";
        var roomTypes = new List<RoomType> { RoomType.Standard };

        repository.SetEmployeePolicy(employeeId, roomTypes);

        repository.HasEmployeePolicy(employeeId).ShouldBeTrue();
        repository.IsRoomTypeAllowedForEmployee(employeeId, RoomType.Standard).ShouldBeTrue();
        repository.IsRoomTypeAllowedForEmployee(employeeId, RoomType.JuniorSuite).ShouldBeFalse();
    }

    [Test]
    public void return_false_when_company_policy_does_not_exist()
    {
        var companyId = "Company44";
        repository.HasCompanyPolicy(companyId).ShouldBeFalse();
    }

    [Test]
    public void return_false_when_employee_policy_does_not_exist()
    {
        var employeeId = "Employee44";
        repository.HasEmployeePolicy(employeeId).ShouldBeFalse();
    }
}

