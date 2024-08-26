namespace FTS.ATBS.Repository;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class ValidationRule(string name, string type, string msg) : Attribute
{
    public override string ToString()
    {
        return $"""
               {name}: 
                 * {type}
                 * {msg}
               
               """;
    }
}