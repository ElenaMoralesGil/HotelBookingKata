using System.Net;
using HotelBookingKata.Entities;
using HotelBookingKata.Controllers;
using System.Net.Http.Json;
using Shouldly;


namespace HotelBookingKata.Test;

class BookingPolicyApiShould { 

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
            new { RoomType = new[] { RoomType.Standard } });

        var response = await client.GetAsync($"/api/booking-policies/employees/{employeeId}/rooms/{RoomType.Standard}/allowed");
        var isAllowed = await response.Content.ReadFromJsonAsync<bool>();

        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        isAllowed.ShouldBeTrue();
    }
}