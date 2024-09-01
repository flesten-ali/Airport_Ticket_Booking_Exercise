using CsvHelper.Configuration;
namespace FTS.ATBS.BookingManagement;
public class FlightMap:ClassMap<Flight>
{
    public FlightMap()
    {
        Map(f => f.DepartureCountry).Name("Departure Country");
        Map(f => f.DestinationCountry).Name("Destination Country");
        Map(f => f.DepartureDate).Name("Departure Date");
        Map(f => f.DepartureAirport).Name("Departure Airport");
        Map(f => f.ArrivalAirport).Name("Arrival Airport");
        Map(f => f.FlightClass).Name("Flight Class");
    }
}
