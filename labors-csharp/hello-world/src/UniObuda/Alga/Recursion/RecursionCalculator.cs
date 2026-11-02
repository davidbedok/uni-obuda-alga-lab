using System.Collections.Concurrent;
using System.Collections.Generic;

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

    public static long Fibonacci(int number)
    {
        if (number <= 1)
        {
            return number;
        }
        return Fibonacci(number - 1) + Fibonacci(number - 2);
    }

    private static Dictionary<int, long> fibonacciMemo = new Dictionary<int, long>();

    public static long FibonacciMemoized(int number)
    {
        if (number <= 1)
        {
            return number;
        }
        if (fibonacciMemo.ContainsKey(number))
        {
            // return fibonacciMemo[number];
            return fibonacciMemo.GetValueOrDefault(number);
        }
        long result = FibonacciMemoized(number - 1) + FibonacciMemoized(number - 2);
        // fibonacciMemo[number] = result;
        fibonacciMemo.Add(number, result);
        return result;
    }

    private static ConcurrentDictionary<int, long> fibonacciMemoConcurrent = new ConcurrentDictionary<int, long>
    {
        [0] = 0,
        [1] = 1
    };

    public static long FibonacciMemoizedConcurrent(int number)
    {
        if (fibonacciMemoConcurrent.TryGetValue(number, out long cachedResult))
        {
            return cachedResult;
        }

        long result = FibonacciMemoizedConcurrent(number - 1) + FibonacciMemoizedConcurrent(number - 2);
        fibonacciMemoConcurrent[number] = result;
        return result;
    }

    public static long FibonacciSelfMemorized(int number)
    {
        var memo = new Dictionary<int, long>();
        return FibonacciSelfMemorized(number, memo);
    }

    private static long FibonacciSelfMemorized(int number, Dictionary<int, long> memo)
    {
        if (number <= 1)
        {
            return number;
        }

        if (memo.TryGetValue(number, out long cachedResult))
        {
            return cachedResult;
        }
        long result = FibonacciSelfMemorized(number - 1, memo) + FibonacciSelfMemorized(number - 2, memo);        
        memo[number] = result;
        return result;
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