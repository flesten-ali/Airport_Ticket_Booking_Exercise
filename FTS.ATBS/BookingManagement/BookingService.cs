using FTS.ATBS.Users;
namespace FTS.ATBS.BookingManagement;
 
public static class BookingService
{
   
    private static readonly List<Booking> AllBookings =new ();
    public static void BookFlight(Passenger passenger, Flight flight)
    {
        var booking = new Booking()
        {
            Passenger = passenger,
            Flight = flight,
            BookingDate = DateTime.Now,
            Class = flight.FlightClass,
            Price = Flight.FlightClasses[flight.FlightClass],
            PassengerId = passenger.PassengerId,
            FlightId = flight.FlightId,
        };
        AllBookings.Add(booking);
        passenger.Bookings.Add(booking);
        flight.Bookings.Add(booking);
    }
    public static List<Booking> GetAllBookings()
    {
        return AllBookings;  
    }
}
 
