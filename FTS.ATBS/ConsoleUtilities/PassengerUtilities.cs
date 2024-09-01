using FTS.ATBS.BookingManagement;
using FTS.ATBS.Extenstions;
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
            var allFlights = flights.Where(f => f.DepartureCountry.IsEqualIgnoreCase(departureCountry) &&
                           f.DestinationCountry.IsEqualIgnoreCase(destinationCountry)&&
                           f.DepartureAirport.IsEqualIgnoreCase(departureAirport)&&
                           f.ArrivalAirport.IsEqualIgnoreCase(arrivalAirport)&&
                           f.FlightClass.ToString().IsEqualIgnoreCase(flightClass.ToString())&&
                           f.DepartureDate == departureDate).ToList();
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
        if (!PassengerHasBookings(passenger))
        {
            StartPassengerMenu(passenger);
            return;
        }
     
        Validation.Errors.Clear();
        var booking = ChooseBookingToModify(passenger);

        if (booking == null)
        {
            Console.WriteLine("Cannot update booking: No matching booking found.");
            StartPassengerMenu(passenger);
            return;
        }

        ModifyBookingDetails(booking);

        if (Validation.Errors.Any())
        {
            PrintConfig.PrintList(Validation.Errors);
            Console.WriteLine("Please try again!");
        }
        else
        {
            Console.WriteLine("Thanks! Booking modified successfully!");
        }

        StartPassengerMenu(passenger);
    }

    private static bool PassengerHasBookings(Passenger passenger)
    {
        return ViewPersonalBookings(passenger);
    }

    private static Booking ChooseBookingToModify(Passenger passenger)
    {
        Console.WriteLine("Choose Booking Number you want to modify:");
        return GetBooking(passenger.Bookings);
    }

    private static void ModifyBookingDetails(Booking booking)
    {
        ModifyDepartureCountry(booking);
        ModifyDestinationCountry(booking);
        ModifyClassType(booking);
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
        if (passengerBookings.Count > 0)
        {
            PrintConfig.PrintList(passengerBookings);  
            return true;
        }
        Console.WriteLine("There is no personal Bookings");
        return false;
    }
}
 
