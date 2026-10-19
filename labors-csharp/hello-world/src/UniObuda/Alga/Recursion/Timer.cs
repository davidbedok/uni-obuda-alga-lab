namespace HelloWorld.UniObuda.Alga.Recursion;

using System;

public class Timer
{

    public static void CountDown(int number)
    {
        if (number == 0)
        {
            Console.WriteLine("Bang!");
            return;
        }
        Console.WriteLine(number);
        CountDown(number - 1);
    }

}