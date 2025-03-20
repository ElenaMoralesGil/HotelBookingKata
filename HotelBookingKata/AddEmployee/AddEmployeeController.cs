using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.AddEmployee;

[ApiController]
[Route("api/companies")]
public class AddEmployeeController : ControllerBase
{

    private AddEmployeeUseCase useCase;

    public AddEmployeeController(AddEmployeeUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpPost("{companyId}/employees")]
    public IActionResult AddEmployee(string companyId, [FromBody] AddEmployeeRequest request)
    {
        try
        {
            useCase.Execute(companyId, request);
            return Created($"/api/companies/{companyId}/employees/{request.EmployeeId}", null);
        }
        catch (EmployeeAlreadyExistsException exception)
        {
            return Conflict(new { message = exception.Message });
        }
    }
}
