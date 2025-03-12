using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.Services;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase {

    private CompanyService companyService;

    public CompaniesController(CompanyService companyService)
    {
        this.companyService = companyService;
    }

    [HttpPost("{companyId}/employees")]
    public IActionResult AddEmployee(string companyId, [FromBody] AddEmployeeRequest request)
    {
        try
        {
            companyService.AddEmployee(companyId, request.EmployeeId);
            return Created($"/api/companies/{companyId}/employees/{request.EmployeeId}", null);
        }
        catch (EmployeeAlreadyExistsException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }
}

public record AddEmployeeRequest(string EmployeeId);

