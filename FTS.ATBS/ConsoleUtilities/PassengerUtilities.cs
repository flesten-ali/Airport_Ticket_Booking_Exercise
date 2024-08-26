using FTS.ATBS.BookingManagement;
using FTS.ATBS.Print;
using FTS.ATBS.Repository;
using FTS.ATBS.Users;
namespace FTS.ATBS.ConsoleUtilities;
public static partial class Utilities
{
    private static List<Flight>? SearchforAvailableFlights(Passenger passenger)
    {
        var flights = FlightRepository.Flights;
        if ( flights.Count == 0)
        {
            Console.WriteLine("There is no available flights!");
            return null;
        }
        Validation.Errors.Clear();

        var departureCountry = GetDepartureCountry();
        var destinationCountry = GetDestinationCountry();
        var departureDate = GetDepartureDate();
        var departureAirport = GetDepartureAirport();
        var arrivalAirport = GetArrivalAirport();
        var flightClass = GetClass();

        if (Validation.Errors.Count > 0)
        {
            PrintConfig.PrintList(Validation.Errors);
        }
        else
        {
            var allFlights = flights.Where(f =>string.Equals( f.DepartureCountry,departureCountry,StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals( f.DestinationCountry , destinationCountry , StringComparison.CurrentCultureIgnoreCase)&&
                string.Equals ( f.DepartureAirport , departureAirport , StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(  f.ArrivalAirport , arrivalAirport, StringComparison.CurrentCultureIgnoreCase) &&
                string.Equals(  f.FlightClass , flightClass, StringComparison.CurrentCultureIgnoreCase)
                && f.DepartureDate == departureDate
            ).ToList();
            if (allFlights.Count > 0)
            {
                PrintConfig.PrintList(allFlights);
                return allFlights;
            }
            Console.WriteLine("There is no available flights!");
        }
        return null;
    }
    private static Booking? GetBooking(List<Booking>passengerBookings)
    {
        var num = Console.ReadLine();
        int.TryParse(num, out var res);
        var booking = passengerBookings.FirstOrDefault(booking => booking.BookingId == res);
        return booking;
    }
    private static void ModifyDepartureCountry(Booking booking)
    {
        Console.WriteLine("Do you want to Modify Departure Country? y/n ");
        var choice = Console.ReadLine();
        if (string.Equals(choice, "Y", StringComparison.InvariantCultureIgnoreCase))
        {
            var departure = GetDepartureCountry();
            booking.Flight.DepartureCountry = departure;
        }
    }
    private static void ModifyDestinationCountry(Booking booking)
    {
        Console.WriteLine("Do you want to Modify Destination Country? y/n");
        var choice = Console.ReadLine();
        if (string.Equals(choice, "Y", StringComparison.InvariantCultureIgnoreCase))
        {
            var destination = GetDestinationCountry();
            booking.Flight.DestinationCountry = destination;
        }
    }
    private static void ModifyClassType(Booking booking)
    {
        Console.WriteLine("Do you want to Modify Class Type? y/n ");
        var choice = Console.ReadLine();
        if (string.Equals(choice, "Y", StringComparison.InvariantCultureIgnoreCase))
        {
            var classType = GetClass();
            booking.Class = classType;
        }
    }
    private static void ModifyBooking(Passenger passenger)
    {
        var exist = ViewPersonalBookings(passenger);
        Validation.Errors.Clear();
        if (exist)
        {
            var passengerBookings = passenger.Bookings;
            Console.WriteLine("Choose Booking Number you want to Modify");
            var booking = GetBooking(passengerBookings);
            if (booking != null)
            {
                ModifyDepartureCountry(booking);
                ModifyDestinationCountry(booking);
                ModifyClassType(booking);

                if (Validation.Errors.Count > 0)
                {
                    PrintConfig.PrintList(Validation.Errors);
                    Console.WriteLine("Please try again!");
                }
                else 
                Console.WriteLine("Thanks! Booking Modified Successfully!");
            }
            else
            {
                Console.WriteLine("Can not update Booking, There is No Booking match!");
            }
        }
        StartPassengerMenu(passenger);  
    }
    private static void CancelBooking(Passenger passenger)
    {
        var exist = ViewPersonalBookings(passenger);
        if (exist)
        {
            Console.WriteLine("Choose Booking Number you want to cancel");
            var passengerBookings = passenger.Bookings;
            var result = GetBooking(passengerBookings);
            if (result != null)
            {
                passengerBookings.Remove(result);
                Console.WriteLine("Booking Canceled Successfully!");
            }
            else
            {
                Console.WriteLine(Environment.NewLine+ "Can not cancel Booking, There is No Booking match!"+ Environment.NewLine );
            }
        } 
        StartPassengerMenu(passenger);
    }
    private static void BookAFlight(Passenger passenger)
    {
        var allFlights = SearchforAvailableFlights(passenger);
        if (allFlights == null || allFlights.Count <= 0) return;
        Console.WriteLine("Enter Flight Number you want to book");
        var num = Console.ReadLine();
        int.TryParse(num, out var res);
        var flight = allFlights.FirstOrDefault(flight => flight.FlightId == res);
        if (flight == null) return;
        BookingService.BookFlight(passenger, flight);
        Console.WriteLine("Booked Successfully!");
    }
    private static bool ViewPersonalBookings(Passenger passenger)
    {
        var passengerBookings = passenger.Bookings;
        if (passengerBookings.Count  > 0 )
        {
            PrintConfig.PrintList(passengerBookings);  
            return true;
        }
        Console.WriteLine("There is no personal Bookings");
        return false;
    }
}
 
