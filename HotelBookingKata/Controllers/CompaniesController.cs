using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/companies")]
public class CompaniesController : ControllerBase
{

    private CompanyService companyService;

    public CompaniesController(CompanyService companyService)
    {
        this.companyService = companyService;
    }

    [HttpDelete("{companyId}/employees/{employeeId}")]
    public IActionResult DeleteEmployee(string employeeId)
    {
        try
        {
            companyService.DeleteEmployee(employeeId);
            return Ok();
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}

