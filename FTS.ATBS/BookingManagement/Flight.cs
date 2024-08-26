using FTS.ATBS.Repository;
namespace FTS.ATBS.BookingManagement;
#nullable disable
public class Flight
{
    public int FlightId { get ; }
    
    [ValidationRule("Destination Country" , "Free Text" , "Required")]
    public string DestinationCountry { get; set; }

    [ValidationRule("Departure Country", "Free Text", "Required")]
    public string DepartureCountry { get; set; }

    [ValidationRule(@"Departure Date", "Date Time", "Required, Allowed Range (today  => future)")]
    public DateTime DepartureDate { get; init; }

    [ValidationRule("Departure Airport", "Free Text", "Required")]
    public string DepartureAirport { get; init; }

    [ValidationRule("Destination Airport", "Free Text", "Required")]
    public string ArrivalAirport { get; init; }
 
    [ValidationRule("Flight Class", "Free Text", "Required, must be [Economy or Business or FirstClass]")]
    public string FlightClass { get; init; }
    public List<Booking> Bookings { get; } = new();

    public static Dictionary<string , double> FlightClasses = new()
    {   
        { "Economy", 100 },
        { "Business", 200.34 },
        { "FirstClass", 300.89 }
    };
    private static int _flightId = 1;
    public Flight()
    {
        FlightId = _flightId++;
    }
    public override string ToString()
    {
        return $"""

            Flight Number: {FlightId}
            Departure Date: {DepartureDate}
            Flight Class {FlightClass}, Price: ${FlightClasses[FlightClass]}
            from :{DepartureCountry} to:{DestinationCountry}  
            Departure Airport: {DepartureAirport} Arrival Airport: {ArrivalAirport}

            """;
    }
}
