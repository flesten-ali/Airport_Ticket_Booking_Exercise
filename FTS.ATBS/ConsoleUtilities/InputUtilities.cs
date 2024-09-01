using FTS.ATBS.BookingManagement;
using FTS.ATBS.Repository;
namespace FTS.ATBS.ConsoleUtilities;
public static partial class Utilities
{
    private static int GetPassenger()
    {  
        Console.WriteLine("Enter Passenger Id:");
        var passenger = Console.ReadLine();
        int.TryParse(passenger, out var res);
        return res;
    }

    private static double GetPrice()
    {
        Console.WriteLine("Enter Price:");
        var price = Console.ReadLine();
        double.TryParse(price, out var priceRes);
        return priceRes;
    }

    private static ClassType GetClass()
    {
        Console.WriteLine("Enter Class:");
        var className = Console.ReadLine() ?? "";
        var flightClass = Validation.ValidateClass(className);
        Validation.ValidateString(className, "Class Type can not be empty");
        return flightClass;
    }

    private static string GetArrivalAirport()
    {
        Console.WriteLine("Enter Arrival Airport:");
        var arrivalAirport = Console.ReadLine() ?? "";
        Validation.ValidateString(arrivalAirport, "Arrival Airport can not be empty");
        return arrivalAirport;
    }

    private static string GetDepartureAirport()
    {
        Console.WriteLine("Enter Departure Airport:");
        var departureAirport = Console.ReadLine() ?? "";
        Validation.ValidateString(departureAirport, "Departure Airport can not be empty");
        return departureAirport;
    }

    private static DateTime GetDepartureDate()
    {
        Console.WriteLine("Enter Departure Date:");
        var date = Console.ReadLine() ?? "";
        Validation.ValidateString(date, "Departure Date can not be empty ");
        var departureDate = Validation.ValidateDate(date);
        return departureDate;
    }

    private static string GetDestinationCountry()
    {  
        Console.WriteLine("Enter Destination Country:");
        var destinationCountry = Console.ReadLine() ?? "";
        Validation.ValidateString(destinationCountry, "Destination Country can not be empry");
        return destinationCountry;
    }

    private static string GetDepartureCountry()
    {
        Console.WriteLine("Enter Departure Country:");
        var departureCountry = Console.ReadLine() ??"";
        Validation.ValidateString(departureCountry, "Departure Country can not be empty ");
        return departureCountry;
    }
}
