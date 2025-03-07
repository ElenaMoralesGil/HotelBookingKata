using Microsoft.AspNetCore.Mvc;
using HotelBookingKata.services;
namespace HotelBookingKata.Controllers;

[ApiController]
[Route("api/hotels")]
public class HotelController :  ControllerBase{
    private HotelService hotelService;

    public HotelController( HotelService hotelService){
        this.hotelService=hotelService;
    }

    [HttpPost]
    public IActionResult AddHotel([FromBody] AddHotelRequest request){
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

    public class AddHotelRequest{
        public string Id { get;set;}
        public string Name { get;set;}
    }
}