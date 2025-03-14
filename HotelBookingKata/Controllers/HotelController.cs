using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using HotelBookingKata.Services;
using Microsoft.AspNetCore.Mvc;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController : ControllerBase
{
    private HotelService hotelService;

    public HotelController(HotelService hotelService)
    {
        this.hotelService = hotelService;
    }

    [HttpPost]
    public IActionResult AddHotel([FromBody] AddHotelRequest request)
    {
        try
        {
            hotelService.AddHotel(request.Id, request.Name);
            return Created($"/api/hotels/{request.Id}", null);
        }
        catch (HotelAlreadyExistsException exception)
        {
            return Conflict(new { message = exception.Message });
        }

    }

    [HttpPut("{hotelId}/rooms/{roomType}")]
    public IActionResult SetRoom(string hotelId, RoomType roomType, [FromBody] SetRoomNumberRequest request)
    {

        try
        {
            hotelService.SetRoom(hotelId, request.Number, roomType);
            return NoContent();
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }

    [HttpGet("{hotelId}")]
    public IActionResult GetHotel(string hotelId)
    {
        try
        {
            var hotel = hotelService.FindHotelBy(hotelId);
            var rooms = hotel.GetRooms().Select(room => new RoomResponse
            {
                Number = room.Number,
                Type = room.Type
            }).ToList();


            return Ok(new HotelResponse
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Rooms = rooms
            });
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }

    }

    public class AddHotelRequest
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }

    public class SetRoomNumberRequest
    {
        public string Number { get; set; }
    }
}
public class HotelResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public List<RoomResponse> Rooms { get; set; }
}

public class RoomResponse
{
    public string Number { get; set; }
    public RoomType Type { get; set; }
}
