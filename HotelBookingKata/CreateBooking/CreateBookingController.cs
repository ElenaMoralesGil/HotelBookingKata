using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingKata.CreateBooking;
[ApiController]
[Route("api/bookings")]
public class CreateBookingController : ControllerBase
{
    private CreateBookingUseCase useCase;

    public CreateBookingController(CreateBookingUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] CreateBookingRequest request)
    {
        try
        {
            var booking = useCase.Execute(request);

            return Created($"/api/bookings/{booking.Id}", booking);
        }
        catch (InvalidBookingDateException exception)
        {
            return Conflict(new { message = exception.Message });
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch (RoomTypeNotAvailableException exception)
        {
            return Conflict(new { message = exception.Message });
        }
        catch (BookingNotAllowedException exception)
        {
            return Conflict(new { message = exception.Message });
        }
        catch (NoRoomsAvailableException exception)
        {
            return Conflict(new { message = exception.Message });
        }

    }
}