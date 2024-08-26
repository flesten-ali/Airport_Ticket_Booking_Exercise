using FTS.ATBS.BookingManagement;
using FTS.ATBS.Print;
namespace FTS.ATBS.Repository;
public static class FlightRepository
{   
    public static List<Flight> Flights  { get; } = new ();
    public static List<Flight>? UploadFlights(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Path does not Exist!");
            return null; 
        }
        var lines  = File.ReadAllLines (path);
        foreach (var line in lines)
        {
            var flightLine = line.Split(",");
            if (flightLine.Length <= 0) continue;

            var departureCountry = flightLine[0];
            Validation.ValidateString(departureCountry, "* Departure Country can not be empty ");
 
            var destinationCountry= flightLine[1];
            Validation.ValidateString(destinationCountry, "* Destination Country can not be empry");

            var departureDate= Validation.ValidateDate(flightLine[2]);

            var departureAirport = flightLine[3];
            Validation.ValidateString(departureAirport, "* Departure Airport can not be empty"); 
               
            var arrivalAirport = flightLine[4];
            Validation.ValidateString(arrivalAirport, "* Arrival Airport can not be empty");
                     
            var flightClass = Validation.ValidateClass(flightLine[5]);
               
            if(Validation.Errors.Count > 0)
            {
                PrintConfig.PrintList(Validation.Errors);
                Console.WriteLine("Please Correct Data and Try again!");
                return null;
            }
            else
            {
                var flight = new Flight()
                {
                    DepartureCountry = departureCountry,
                    DestinationCountry = destinationCountry,
                    ArrivalAirport = arrivalAirport,
                    DepartureAirport = departureAirport,
                    DepartureDate = departureDate,
                    FlightClass = flightClass,
                };
                Flights.Add(flight);  
            }
        }
        Validation.Errors.Clear();
        Console.WriteLine(Environment.NewLine+"Flights Uploaded Successfully!"+Environment.NewLine);
        return Flights;
    }
}

 