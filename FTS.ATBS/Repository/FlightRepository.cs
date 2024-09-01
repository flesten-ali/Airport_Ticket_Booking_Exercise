using CsvHelper;
using FTS.ATBS.BookingManagement;
using FTS.ATBS.Print;
using System.Globalization;
using System.Reflection.PortableExecutable;
namespace FTS.ATBS.Repository;
public static class FlightRepository
{   
    public static List<Flight> Flights  { get; private set; } = new ();
    public static List<Flight>? UploadFlights(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Path does not Exist!");
            return null;
        }

        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            csv.Context.RegisterClassMap<FlightMap>();
            while (csv.Read())
            {
                var flight = csv.GetRecord<Flight>();
                bool isValidFlight = ValidateFlight(flight);
                if (isValidFlight) Flights.Add(flight);
            }
        }

        if (Validation.Errors.Count > 0)
        {
            PrintConfig.PrintList(Validation.Errors);
            Console.WriteLine("Please Correct the Wrong Data and Try again!");
            return null;
        }

        Validation.Errors.Clear();

        Console.WriteLine(Environment.NewLine + "Flights Uploaded Successfully!" + Environment.NewLine);
        return Flights;
    }

    private static bool ValidateFlight(Flight flight)
    {
        Validation.ValidateString(flight.DepartureCountry, "* Departure Country cannot be empty");
        Validation.ValidateString(flight.DestinationCountry, "* Destination Country cannot be empty");
        Validation.ValidateString(flight.DepartureAirport, "* Departure Airport cannot be empty");
        Validation.ValidateString(flight.ArrivalAirport, "* Arrival Airport cannot be empty");
        Validation.ValidateClass(flight.FlightClass.ToString());
        if (Validation.Errors.Count > 0)
        {
            return false;
        }
        return true;

    }
}

 