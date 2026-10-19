namespace HelloWorld.UniObuda.Alga.Recursion;

public class RecursionCalculator
{

    public static long Factorial(int number)
    {
        if (number <= 1)
        {
            return 1;
        }
        return number * Factorial(number - 1);
    }

    public static int SumDigits(int number)
    {
        if (number < 10)
        {
            return number;
        }
        return number % 10 + SumDigits(number / 10);
    }

    public static int Fibonacci(int number)
    {
        if (number <= 1)
        {
            return number;
        }
        return Fibonacci(number - 1) + Fibonacci(number - 2);
    }

    public static int AddPositives(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        return AddPositives(a + 1, b - 1);
    }

    public static int Add(int a, int b)
    {
        if (b == 0)
        {
            return a;
        }
        if (b > 0)
        {
            return Add(a + 1, b - 1);
        }
        else
        {
            return Add(a - 1, b + 1);
        }
    }

    public static int Negate(int a)
    {
        if (a == 0) {
            return 0;
        }
        if (a > 0)
        {
            return Add(-1, Negate(a - 1));
        }
        else
        {
            return Add(1, Negate(a + 1));
        }
    }

    public static int Multiply(int a, int b)
    {
        if (b == 0)
        {
            return 0;
        }
        if (b > 0)
        {
            // return a + Multiply(a, b - 1);
            return Add(a, Multiply(a, b - 1));
        }
        else
        {
            return Negate(Multiply(a, Negate(b)));
        }
    }

    public static int Power(int a, int b)
    {
        if (b == 0)
        {
            return 1;
        }
        // return a * Power(a, b - 1);
        return Multiply(a, Power(a, b - 1));
    }

}