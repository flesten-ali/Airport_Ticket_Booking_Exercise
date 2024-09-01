using FTS.ATBS.Users;
namespace FTS.ATBS.ConsoleUtilities;
public static partial class Utilities
{
    public static void Start()
    {
        while (true)
        {
            var role = ChooseRole();
            switch (role)
            {
                case "1":
                    var manager = new Manager();
                    StartManagerMenu(manager);
                    break;
                case "2":
                    var passenger = new Passenger();
                    StartPassengerMenu(passenger);
                    break;
                case "-1":
                    break;
                default:
                    continue;
            }
            break;
        }
    }

    private static string ChooseRole()
    {
        Console.WriteLine("""  
                      Choose your role:
                         1- Manager
                         2- Passenger
                         or -1 to Exit
                      """);
        var role = Console.ReadLine();
        if (role == "-1")
        {
            return role;
        }
        while (role != "1" && role != "2")
        {
            if (role == "-1") break;
            Console.WriteLine("Please enter valid role or enter -1 to exit");
            role = Console.ReadLine();
        }
        return role;
    }

    private static string GetManagerChoice()
    {
        Console.WriteLine("""
                      Choose number:
                          1- Filter Bookings
                          2- Upload Flights
                           or Enter -1 to Exit
                  """);

        var choice = Console.ReadLine();
        if(choice == "-1") { return choice; }

        while (choice != "1" && choice != "2")
        {
            Console.WriteLine("Please enter valid choice or enter -1 to exit");
            choice = Console.ReadLine();
            if (choice == "-1") break;
        }
        return choice;
    }

    private static string GetPassengerChoice()
    {
        Console.WriteLine("""
                      Choose number:
                      1- Search for Available Flights
                      2- Book a Flight
                      3- View personal bookings
                      4- Cancel a booking
                      5- Modify a booking
                      or Enter -1 to Exit
                  """);

        var choice = Console.ReadLine();
        if (choice == "-1")  return choice;
        while (choice != "1" && choice != "2" && choice != "3" && choice != "4" && choice != "5")
        {
            Console.WriteLine("Please enter valid choice or enter -1 to exit");
            choice = Console.ReadLine();
            if (choice == "-1") break;
        }
        return choice;
    }

    private static void StartManagerMenu(Manager manager)
    {
        while (true)
        {
            var choice = GetManagerChoice();
            switch (choice)
            {  
                case "1":
                    FilterBookings(manager);
                    break;
                case "2":
                    UploadFlights(manager);
                    break;
                case "-1":
                    Start();
                    break;
                default:
                    continue;
            }
            break;
        }
    }

    private static void StartPassengerMenu(Passenger passenger)
    {
        while (true)
        {
            var choice = GetPassengerChoice();
            switch (choice)
            {
                case "1":
                    SearchforAvailableFlights(passenger);
                    continue;
                case "2":
                    BookAFlight(passenger);
                    continue;
                case "3":
                    ViewPersonalBookings(passenger);
                    continue;
                case "4":
                    CancelBooking(passenger);
                    break;
                case "5":
                    ModifyBooking(passenger);
                    break;
                case "-1":
                    Start();
                    break;
                default:
                    continue;
            }
            break;
        }
    }

}
