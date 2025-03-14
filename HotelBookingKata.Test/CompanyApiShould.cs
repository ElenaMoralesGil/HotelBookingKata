using Shouldly;
using System.Net;
using System.Net.Http.Json;


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
            EmployeeId = "Employee1",
        };

        var response = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);

        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        response.Headers.Location?.ToString().ShouldContain("Employee1");
        var companyRepository = factory.GetCompanyRepository();
        var employeeRepository = factory.GetEmployeeRepository();
        companyRepository.GetCompanies().ShouldContainKey(companyId);
        employeeRepository.GetEmployees().ShouldContainKey("Employee1");
    }

    [Test]
    public async Task return_conflict_when_employee_already_exists()
    {
        var companyId = "Company1";
        var employee = new
        {
            EmployeeId = "Employee1",
        };
        var response = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);
        response.StatusCode.ShouldBe(HttpStatusCode.Created);
        var response2 = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);
        response2.StatusCode.ShouldBe(HttpStatusCode.Conflict);

        var companyRepository = factory.GetCompanyRepository();
        var employeeRepository = factory.GetEmployeeRepository();
        companyRepository.GetCompanies().ShouldContainKey(companyId);
        employeeRepository.GetEmployees().ShouldContainKey("Employee1");

    }

    [Test]
    public async Task delete_employee_when_the_employee_exists_with_all_their_policies_and_bookings()
    {
        var companyId = "Company1";
        var employee = new
        {
            EmployeeId = "Employee1",
        };

        var response1 = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);
        var response2 = await client.DeleteAsync($"/api/companies/{companyId}/employees/{employee.EmployeeId}");

        response1.StatusCode.ShouldBe(HttpStatusCode.Created);
        response2.StatusCode.ShouldBe(HttpStatusCode.OK);
        var companyRepository = factory.GetCompanyRepository();
        var employeeRepository = factory.GetEmployeeRepository();
        var bookingPolicyRepository = factory.GetBookingPolicyRepository();
        var bookingRepository = factory.GetBookingRepository();
        companyRepository.GetCompanies().ShouldContainKey(companyId);
        employeeRepository.GetEmployees().ShouldNotContainKey("Employee1");
        bookingPolicyRepository.GetEmployeePolicies().ShouldNotContainKey("Employee1");
        bookingRepository.GetBookings().ShouldNotContainKey("Employee1");
    }

    [Test]
    public async Task return_not_found_when_deleting_non_existing_employee()
    {
        var companyId = "Company1";
        var employee = new
        {
            EmployeeId = "Employee1",
        };
        var response1 = await client.PostAsJsonAsync($"/api/companies/{companyId}/employees", employee);
        var response2 = await client.DeleteAsync($"/api/companies/{companyId}/employees/Employee2");

        response1.StatusCode.ShouldBe(HttpStatusCode.Created);
        response2.StatusCode.ShouldBe(HttpStatusCode.NotFound);
        var companyRepository = factory.GetCompanyRepository();
        var employeeRepository = factory.GetEmployeeRepository();
        companyRepository.GetCompanies().ShouldContainKey(companyId);
        employeeRepository.GetEmployees().ShouldContainKey("Employee1");
    }
}
