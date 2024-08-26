using FTS.ATBS.BookingManagement;
namespace FTS.ATBS.Users;
#nullable disable
public class Passenger 
{
    public int PassengerId { get; }
    public List<Booking> Bookings { get; set; } = new ();

    private static int _countPassengers = 1;
    public Passenger()
    {
        _countPassengers++;
        PassengerId = _countPassengers;
    }
}

