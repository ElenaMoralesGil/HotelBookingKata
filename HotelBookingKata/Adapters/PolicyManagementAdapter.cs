namespace HotelBookingKata.Adapters
{
    public class PolicyManagementAdapter
    {
        private HttpClient client;
        public PolicyManagementAdapter(HttpClient client)
        {
            this.client = client;
        }
        public async Task DeleteEmployeePolicy(string employeeId)
        {
            await client.DeleteAsync($"api/booking-policies/employees/{employeeId}");
        }
    }
}
