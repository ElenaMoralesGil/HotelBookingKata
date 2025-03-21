using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace HotelBookingKata.SetRoom;

[ApiController]
[Route("api/hotels")]
public class SetRoomController :  ControllerBase
{
    private SetRoomUseCase useCase;

    public SetRoomController(SetRoomUseCase useCase)
    {
        this.useCase = useCase;
    }

    [HttpPut("{hotelId}/rooms/{roomType}")]
    public IActionResult SetRoom(string hotelId, RoomType roomType, [FromBody] SetRoomNumberRequest request)
    {

        try
        {
            useCase.Execute(hotelId, request.Number, roomType);
            return NoContent();
        }
        catch (HotelNotFoundException exception)
        {
            return NotFound(new { message = exception.Message });
        }
    }
}
