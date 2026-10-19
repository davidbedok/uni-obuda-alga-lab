namespace HelloWorld.UniObuda.Alga;

using System;

public class AppDecorator
{
    
    public static void Header( string value )
    {
        string border = new string('─', value.Length + 2);
        Console.WriteLine("");
        Console.WriteLine($"┌{border}┐");
        Console.WriteLine($"│ {value} │");
        Console.WriteLine($"└{border}┘");
        Console.WriteLine("");
    }

}
