namespace HotelBookingKata.Adapters
{
    public class BookingManagmentAdapter
    {
        private HttpClient client;

        public BookingManagmentAdapter(HttpClient client)
        {
            this.client = client;
        }

        public async Task DeleteEmployeeBookingAsync(string employeeId)
        {
            await client.DeleteAsync($"api/bookings/employees/{employeeId}");
        }
    }
}
