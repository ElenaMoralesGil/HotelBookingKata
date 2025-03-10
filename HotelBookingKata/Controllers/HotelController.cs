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
            hotelService.SetRoom(hotelId, request.RoomNumber, request.RoomType);
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
            var roomCounts = hotel.GetRooms()
                .GroupBy(room => room.Type)
                .Select(room => new RoomTypeCount{ RoomType = room.Key, Count = room.Count() })
                .ToList();

            return Ok(new HotelResponse
            {
                Id = hotel.Id,
                Name = hotel.Name,
                RoomCounts = roomCounts
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
        public required string RoomNumber { get; set; }
        public required RoomType RoomType { get; set; }
    }
}
public class HotelResponse
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required List<RoomTypeCount> RoomCounts { get; set; }
}

public class RoomTypeCount
{
    public required RoomType RoomType { get; set; }
    public required int Count { get; set; }
}
