using HotelBookingKata.Entities;
using Shouldly;
using System.Net;
using System.Net.Http.Json;


namespace HotelBookingKata.Test;

class BookingPolicyApiShould
{

    private CustomWebApplicationFactory<Program> factory;
    private HttpClient client;

    [SetUp]
    public void Setup()
    {
        factory = new CustomWebApplicationFactory<Program>();
        client = factory.CreateClient();

    }

    [TearDown]
    public void TearDown()
    {
        client?.Dispose();
        factory?.Dispose();
    }

    [Test]
    public async Task allow_booking_when_employee_policy_permits()
    {
        var companyId = "Company1";
        var employeeId = "Employee1";
        await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", new { EmployeeId = employeeId });
        await client.PutAsJsonAsync($"/api/booking-policies/employees/{employeeId}",
            new { RoomTypes = new[] { RoomType.Standard } });

        var response = await client.GetAsync($"/api/booking-policies/employees/{employeeId}/rooms/{RoomType.Standard}/allowed");
        var isAllowed = await response.Content.ReadFromJsonAsync<bool>();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        isAllowed.ShouldBeTrue();
        var repository = factory.GetBookingPolicyRepository();
        repository.IsRoomTypeAllowedForEmployee(employeeId, RoomType.Standard).ShouldBeTrue();
    }

    [Test]
    public async Task deny_booking_when_employee_policy_does_not_permit_and_has_no_company_policy()
    {
        var companyId = "Company1";
        var employeeId = "Employee1";
        await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", new { EmployeeId = employeeId });
        await client.PutAsJsonAsync($"/api/booking-policies/employees/{employeeId}",
            new { RoomTypes = new[] { RoomType.Standard } });

        var response = await client.GetAsync($"/api/booking-policies/employees/{employeeId}/rooms/{RoomType.JuniorSuite}/allowed");

        var isAllowed = await response.Content.ReadFromJsonAsync<bool>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        isAllowed.ShouldBeFalse();
        var repository = factory.GetBookingPolicyRepository();
        repository.IsRoomTypeAllowedForEmployee(employeeId, RoomType.JuniorSuite).ShouldBeFalse();
    }

    [Test]
    public async Task deny_booking_when_employee_policy_does_not_exists_and_company_policy_does_not_permit()
    {
        var companyId = "Company1";
        var employeeId = "Employee1";
        await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", new { EmployeeId = employeeId });
        await client.PutAsJsonAsync($"/api/booking-policies/companies/{companyId}",
            new { RoomTypes = new[] { RoomType.MasterSuite } });

        var response = await client.GetAsync($"/api/booking-policies/employees/{employeeId}/rooms/{RoomType.JuniorSuite}/allowed");

        var isAllowed = await response.Content.ReadFromJsonAsync<bool>();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        isAllowed.ShouldBeFalse();
        var repository = factory.GetBookingPolicyRepository();
        repository.IsRoomTypeAllowedForEmployee(employeeId, RoomType.JuniorSuite).ShouldBeFalse();
    }


}