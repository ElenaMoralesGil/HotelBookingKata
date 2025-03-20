using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.SetCompanyPolicy;

[ApiController]
[Route("api/booking-policies")]
public class SetCompanyPoliciyController : ControllerBase
{
    private SetCompanyPolicyUseCase useCase;

    public SetCompanyPoliciyController(SetCompanyPolicyUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpPut("companies/{companyId}")]
    public IActionResult SetCompanyPolicy(string companyId, [FromBody] SetCompanyPolicyRequest request)
    {
        try
        {
            useCase.Execute(companyId, request);
            return Ok();
        }
        catch (CompanyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}