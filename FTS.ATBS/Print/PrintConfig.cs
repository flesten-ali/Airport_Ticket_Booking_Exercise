namespace FTS.ATBS.Print;
public static class PrintConfig
{  
    public static void PrintList<T>(List<T> list)
    {   
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (var item in list)
        {    
            Console.WriteLine(item);
            Console.WriteLine("***************************");
        }
        Console.ResetColor();
    }   
}
 
