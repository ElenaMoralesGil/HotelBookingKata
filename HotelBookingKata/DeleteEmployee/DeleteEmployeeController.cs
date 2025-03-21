using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.DeleteEmployee;


[ApiController]
[Route("api/companies")]
public class DeleteEmployeeController : ControllerBase
{

    private DeleteEmployeeUseCase useCase;

    public DeleteEmployeeController(DeleteEmployeeUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpDelete("{companyId}/employees/{employeeId}")]
    public IActionResult DeleteEmployee(string employeeId)
    {
        try
        {
            useCase.Execute(employeeId);
            return Ok();
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}
