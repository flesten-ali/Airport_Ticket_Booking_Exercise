
namespace FTS.ATBS.Extenstions;

public static class StringEquality
{
    public static bool IsEqualIgnoreCase(this string s1, string s2)
    {
       return string.Equals(s1,
            s2, StringComparison.CurrentCultureIgnoreCase);
    }
}


