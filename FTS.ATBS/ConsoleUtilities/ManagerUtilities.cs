using FTS.ATBS.BookingManagement;
using FTS.ATBS.Extenstions;
using FTS.ATBS.Print;
using FTS.ATBS.Repository;
using FTS.ATBS.Users;
namespace FTS.ATBS.ConsoleUtilities;
public static partial class  Utilities
{          
    private static void UploadFlights(Manager manager)
    {
        Validation.DynamicValidation();
        Console.WriteLine("Please Enter File Path");
        var path = Console.ReadLine();
        if (path is null)
        {
            Console.WriteLine("Path is not valid");
        }
        else
        {
            FlightRepository.UploadFlights(path);
        }
        StartManagerMenu(manager);
    }

    private static void FilterBookings(Manager manager)
    {
        while (true)
        {
            Console.WriteLine("""
                                Choose parameter number you want to filter using it  : 
                                    1- Price
                                    2- Departure Country
                                    3- Destination Country
                                    4- Departure Date
                                    5- Departure Airport
                                    6- Arrival Airport
                                    7- Passenger
                                    8- Class
                                    Or Enter -1 to Exit
                                """);

            var num = Console.ReadLine();
            switch (num)
            {
                case "1":
                    FilterPrice(manager);
                    break;
                case "2":
                    FilterDepartureCountry(manager);
                    break;
                case "3":
                    FilterDestinationCountry(manager);
                    break;
                case "4":
                    FilterDepartureDate(manager);
                    break;
                case "5":
                    FilterDepartureAirport(manager);
                    break;
                case "6":
                    FilterArrivalAirport(manager);
                    break;
                case "7":
                    FilterPassenger(manager);
                    break;
                case "8":
                    FilterClass(manager);
                    break;
                case "-1":
                    break;
                default:
                    Console.WriteLine("Unvalid input!");
                    continue;
            }
            StartManagerMenu(manager);
            break;
        }
    }

    private static void FilterClass(Manager manager)
    {
        var className = GetClass();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Class == className).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings for {className}  ");
        }   
    }

    private static void FilterPassenger(Manager manager)
    {
        var passengerId = GetPassenger();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Passenger.PassengerId == passengerId).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {    
            Console.WriteLine($"No Bookings for {passengerId}  ");
        }
    }

    private static void FilterArrivalAirport(Manager manager)
    {
        var arrivalAirport = GetArrivalAirport();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Flight.ArrivalAirport.IsEqualIgnoreCase(arrivalAirport)).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with {arrivalAirport} ");
        }
    }
    private static void FilterDepartureAirport(Manager manager)
    {
        var departureAirport = GetDepartureAirport();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Flight.DepartureAirport.IsEqualIgnoreCase(departureAirport)).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with {departureAirport}  ");
        }
    }

    private static void FilterDepartureDate(Manager manager)
    {
        var departureDate = GetDepartureDate();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Flight.DepartureDate == departureDate).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with  {departureDate} ");
        }       
    }

    private static void FilterDestinationCountry(Manager manager)
    {
        var destinationCountry = GetDestinationCountry();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Flight.DestinationCountry.IsEqualIgnoreCase(destinationCountry)).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with   {destinationCountry} ");
        }
    }

    private static void FilterDepartureCountry(Manager manager)
    {
        var departureCountry = GetDepartureCountry();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => departureCountry.IsEqualIgnoreCase(book.Flight.DepartureCountry)).ToList();
        if (res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with {departureCountry} ");
        }
    }

    private static void FilterPrice(Manager manager)
    {
        var price = GetPrice();
        var allBookings = BookingService.GetAllBookings();
        var res = allBookings.Where(book => book.Price == price).ToList();
        if(res.Count > 0)
        {
            PrintConfig.PrintList(res);
        }
        else
        {
            Console.WriteLine($"No Bookings with price ${price}");
        }
    }
}

 