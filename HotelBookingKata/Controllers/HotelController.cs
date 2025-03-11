using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.services;
using HotelBookingKata.Entities;
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
        catch (InvalidOperationException exception)
        {
            return Conflict(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    [HttpPut("{hotelId}/rooms")]
    public IActionResult SetRoom(string hotelId, [FromBody] SetRoomRequest request)
    {

        try
        {
            hotelService.SetRoom(hotelId, request.Number, request.Type);
            return NoContent();
        }
        catch (InvalidOperationException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            return BadRequest(new { message = exception.Message });
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
        catch (InvalidOperationException exception)
        {
            return NotFound(new { message = exception.Message });
        }
        catch (Exception exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }

    public class AddHotelRequest
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
    }

    public class SetRoomRequest
    {
        public  string Number { get; set; }
        public  RoomType Type { get; set; }
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
