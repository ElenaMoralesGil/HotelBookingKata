using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.Services;
using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/bookings")]
public class BookingsController : ControllerBase
{
    private BookingService bookingService;

    public BookingsController(BookingService bookingService)
    {
        this.bookingService = bookingService;
    }

    [HttpPost]
    public IActionResult CreateBooking([FromBody] CreateBookingRequest request)
    {
        try
        {
            var booking = bookingService.Book(
                request.EmployeeId,
                request.HotelId,
                request.RoomType,
                request.CheckIn,
                request.CheckOut);

            return Created($"/api/bookings/{booking.Id}", booking);
        }
        catch(InvalidBookingDateException exception)
        {
            return Conflict(new { message = exception.Message });
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch(RoomTypeNotAvailableException exception)
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

public record CreateBookingRequest(string EmployeeId, string HotelId, RoomType RoomType, DateTime CheckIn, DateTime CheckOut);