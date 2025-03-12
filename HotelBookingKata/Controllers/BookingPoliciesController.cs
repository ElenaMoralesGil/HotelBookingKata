using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.Services;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Entities;

namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/booking-policies")]
public class BookingPoliciesController : ControllerBase
{
    private BookingPolicyService bookingPolicyService;

    public BookingPoliciesController(BookingPolicyService bookingPolicyService)
    {
        this.bookingPolicyService = bookingPolicyService;
    }

    [HttpPut("companies/{companyId}") ]
    public IActionResult SetCompanyPolicy(string companyId, [FromBody] SetCompanyPolicyRequest request)
    {
        try
        {
            bookingPolicyService.SetCompanyPolicy(companyId, request.Policy);
            return Ok();
        }
        catch (CompanyNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpPut("employees/{employeeId}")]
    public IActionResult SetEmployeePolicy(string employeeId, [FromBody] SetEmployeePolicyRequest request)
    {
        try
        {
            bookingPolicyService.SetEmployeePolicy(employeeId, request.Policy);
            return Ok();
        }
        catch (EmployeeNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}

public record SetCompanyPolicyRequest(List<RoomType> Policy);
public record SetEmployeePolicyRequest(List<RoomType> Policy);
