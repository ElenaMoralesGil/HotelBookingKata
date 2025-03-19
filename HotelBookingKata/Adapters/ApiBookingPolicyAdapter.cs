using HotelBookingKata.Entities;
using HotelBookingKata.Exceptions;
using System.Net.Http.Json;
namespace HotelBookingKata.Adapters;


public class ApiBookingPolicyAdapter :  BookingPolicyAdapter
{

    public async Task<bool> IsBookingAllowed(string employeeId, RoomType roomType)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"api/booking-policies/employees/{employeeId}/rooms/{roomType}/allowed");
        if (!response.IsSuccessStatusCode)
        {
            return false;
        }
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}

