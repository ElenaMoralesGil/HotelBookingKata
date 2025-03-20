using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.AddHotel;


[ApiController]
[Route("api/hotels")]
public class AddHotelController : ControllerBase
{
    private AddHotelUseCase useCase;

    public AddHotelController(AddHotelUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpPost]
    public IActionResult AddHotel([FromBody] AddHotelRequest request)
    {
        try
        {
            useCase.Execute(request);
            return Created($"/api/hotels/{request.Id}", null);
        }
        catch (HotelAlreadyExistsException exception)
        {
            return Conflict(new { message = exception.Message });
        }

    }
}

