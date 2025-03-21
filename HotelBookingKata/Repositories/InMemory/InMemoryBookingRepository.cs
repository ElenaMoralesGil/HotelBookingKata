using HotelBookingKata.Entities;
namespace HotelBookingKata.Repositories.InMemory;

public class InMemoryBookingRepository : BookingRepository
{
    private Dictionary<string, Booking> bookings = new Dictionary<string, Booking>();

    public void Add(Booking booking)
    {
        bookings[booking.Id] = booking;
    }

    public int CountBookingsByHotelRoomType(string hotelId, RoomType roomType, DateTime date)
    {
        return bookings.Values.Count(b => b.HotelId == hotelId && b.RoomType == roomType && b.CheckIn <= date && b.CheckOut > date);
    }

    public Dictionary<string, Booking> GetBookings()
    {
        return bookings;
    }

    public bool Exists(string id)
    {
        return bookings.ContainsKey(id);
    }

    public void DeleteEmployeeBookings(string employeeId)
    {
        var bookingsToDelete = bookings.Values.Where(b => b.EmployeeId == employeeId).ToList();
        foreach (var booking in bookingsToDelete)
        {
            bookings.Remove(booking.Id);
        }
    }
}
