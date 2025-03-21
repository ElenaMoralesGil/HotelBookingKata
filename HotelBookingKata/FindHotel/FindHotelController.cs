using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingKata.FindHotel;

[ApiController]
[Route("api/hotels")]
public class FindHotelController : ControllerBase
{
    private FindHotelUseCase useCase;

    public FindHotelController(FindHotelUseCase useCase)
    {
        this.useCase = useCase;
    }



    [HttpGet("{hotelId}")]
    public IActionResult GetHotel(string hotelId)
    {
        try
        {
            var hotel = useCase.Execute(hotelId);
            var rooms = hotel.GetRooms().Select(room => new RoomResponse(room.Number, room.Type)).ToList();

            return Ok(new FindHotelResponse(hotel.Id, hotel.Name, rooms));
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}