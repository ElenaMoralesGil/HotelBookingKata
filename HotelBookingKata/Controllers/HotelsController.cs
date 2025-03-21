using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelsController : ControllerBase
{
    private HotelService hotelService;

    public HotelsController(HotelService hotelService)
    {
        this.hotelService = hotelService;
    }



    [HttpGet("{hotelId}")]
    public IActionResult GetHotel(string hotelId)
    {
        try
        {
            var hotel = hotelService.FindHotelBy(hotelId);
            var rooms = hotel.GetRooms().Select(room => new RoomResponse(room.Number, room.Type)).ToList();

            return Ok(new HotelResponse(hotel.Id, hotel.Name, rooms));
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}