using System.Net;
using HotelBookingKata.Entities;
using HotelBookingKata.Controllers;
using System.Net.Http.Json;
using Shouldly;


namespace HotelBookingKata.Test;
    class CompanyApiShould
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
    public async Task add_employee_when_valid_employee_is_created()
    {
        var companyId = "Company1";
        var employee = new
        {
            id = "Employee1",
        };

        var response = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location.ToString().ShouldContain("Employee1");
        var repository = factory.GetRepository();
        repository.Exists(employee.id).ShouldBeTrue();
    }
}
