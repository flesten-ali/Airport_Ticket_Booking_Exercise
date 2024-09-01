using FTS.ATBS.BookingManagement;
using FTS.ATBS.Extenstions;
using System.Globalization;
using System.Reflection;
namespace FTS.ATBS.Repository;
public static class Validation
{
    public static readonly List<string> Errors = new();
    public static bool ValidateString(string target, string errorMsg)
    {
        if (target.Length != 0) return true;
        Errors.Add(errorMsg);
        return false;
    }

    public static DateTime ValidateDate(string target)
    {
        var result = DateTime.
        TryParseExact(target, "d/M/yyyy",
        CultureInfo.InvariantCulture,
        DateTimeStyles.None,
        out var departureDate);

        var isValid = departureDate >= DateTime.Now;
        if (result)
        {
            if (!isValid)
                Errors.Add("Departure Date Allowed Range (today -> future)");
        }
        else
        {
            Errors.Add("Departure Date must be in format dd/MM/yyyy");
        }
        return departureDate;
    }

    public static ClassType ValidateClass(string flightClass)
    {
        if (!Flight.FlightClasses.Keys.Any(key => key.ToString().IsEqualIgnoreCase(flightClass)))
        {
            Errors.Add("Class Type, must be [Economy or Business or FirstClass]");
        }
        Enum.TryParse<ClassType>(flightClass , out var classType);
        return classType ;
    }

    public static void DynamicValidation()
    {
        var flightType = typeof(Flight);
        var flightProp = flightType.GetProperties();
        foreach (var prop in flightProp)
        {
            var validation = prop.GetCustomAttribute(typeof(ValidationRule), false);
            if (validation is ValidationRule customAttr)
            {
                Console.WriteLine(customAttr);
            }
        }
    }
}
 
