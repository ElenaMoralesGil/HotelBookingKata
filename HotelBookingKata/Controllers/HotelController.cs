using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.services;
using HotelBookingKata.Entities;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController :  ControllerBase{
    private HotelService hotelService;

    public HotelController( HotelService hotelService) {
        this.hotelService=hotelService;
    }

    [HttpPost]
    public IActionResult AddHotel([FromBody] AddHotelRequest request) {
        try{
            hotelService.AddHotel(request.Id, request.Name);
            return Created($"/api/hotels/{request.Id}", null);
        }
        catch(InvalidOperationException exception){
            return Conflict(new { message = exception.Message});
        }
        catch(Exception exception){
            return BadRequest(new { message = exception.Message});
        }
    }

    [HttpPut("{hotelId}/rooms")]
    public IActionResult SetRoom(string hotelId, [FromBody] SetRoomRequest request) {
        
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

    public class AddHotelRequest {
        public required string Id { get;set;}
        public required string Name { get;set;}
    }

    public class SetRoomRequest {
        public required string RoomNumber { get; set; }
        public required RoomType RoomType { get; set; }
    }
}