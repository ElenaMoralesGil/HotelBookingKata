using HotelBookingKata.Entities;
namespace HotelBookingKata.Adapters;


public class ApiBookingPolicyAdapter : BookingPolicyAdapter
{

    public async Task<bool> IsBookingAllowed(string employeeId, RoomType roomType)
    {
        var client = new HttpClient();
        var response = await client.GetAsync($"api/booking-policies/employees/{employeeId}/rooms/{roomType}/allowed");
        return await response.Content.ReadFromJsonAsync<bool>();
    }
}

