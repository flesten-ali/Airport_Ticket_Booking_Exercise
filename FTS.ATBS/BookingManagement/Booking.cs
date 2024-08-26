using FTS.ATBS.Users;
namespace FTS.ATBS.BookingManagement;
#nullable disable
public class Booking
{
    public int BookingId { get; set; }
    public int PassengerId { get; set; }
    public int FlightId { get; init; }
    public Passenger Passenger { get; init; }
    public Flight Flight { get; init; }
    public DateTime BookingDate { get; init; }
    public string Class { get; set; }
    public double Price { get; init; }

    private static int _bookingNumber = 0;

    public Booking()
    {
        _bookingNumber++;
         BookingId = _bookingNumber;
    }
    public override string ToString()
    {
        return $"""       

                   * Booking Number: {BookingId}
                      Passenger Id: {Passenger.PassengerId}
                      Booking Date: {BookingDate}
                      Flight Number: {FlightId} 
                      Destination Country: {Flight.DestinationCountry}
                      Class: {Class}
                      Price: {Price}

               """;
    }

}

