namespace HelloWorld.UniObuda.Alga.Recursion;

using System;

public class DemoRecursion
{

    public static void CountDownDemo()
    {
        AppDecorator.Header("Recursion: count down");
        Timer.CountDown(5);
    }

    public static void FactorialDemo()
    {
        // 0! = 1
        // 1! = 1 = 1 * 0! (or def. 1)
        // 2! = 2 × 1 = 2 = 2 * 1!
        // 3! = 3 × 2 × 1 = 6 = 3 * 2!
        // 4! = 4 × 3 × 2 × 1 = 24 = 4 * 3!
        // n! = n × (n − 1) × (n − 2) × ... × 3 × 2 × 1 = n * (n-1)!
        AppDecorator.Header("Recursion: factorial");
        for (int number = 0; number <= 6; number++)
        {
            Console.WriteLine($"{number}! = {RecursionCalculator.Factorial(number)}");
        }
    }

    public static void FibonacciDemo()
    {
        // Fibonacci(0) = 0
        // Fibonacci(1) = 1
        // Fibonacci(2) = 0 + 1 = 1
        // Fibonacci(3) = 1 + 1 = 2
        // Fibonacci(4) = 1 + 2 = 3
        // Fibonacci(5) = 2 + 3 = 5 = Fibonacci(4) + Fibonacci(3)
        // Fibonacci(6) = 3 + 5 = 8 = Fibonacci(5) + Fibonacci(4)
        // Fibonacci(7) = 5 + 8 = 13 = Fibonacci(6) + Fibonacci(5)
        // Fibonacci(n) = Fibonacci(n-1) + Fibonacci(n-2)
        AppDecorator.Header("Recursion: Fibonacci call tree");
        for (int number = 0; number <= 8; number++)
        {
            Console.WriteLine($"Fibonacci({number}) = {RecursionCalculator.Fibonacci(number)}");
        }
    }

    public static void CalculationDemo()
    {
        AppDecorator.Header("Recursion: Calculation");
        // Add(5, 3) = 8 = Add(6, 2) = Add(7, 1) = Add(8, 0) = 8
        Console.WriteLine($"5 + 3 = Add(5, 3) = {RecursionCalculator.Add(5, 3)}");
        // Add(5, -3) = 2 = Add(4, -2) = Add(3, -1) = Add(2, 0) = 2
        Console.WriteLine($"5 - 3 = Add(5, -3) = {RecursionCalculator.Add(5, -3)}");

        // Negate(5) = -5 = -1 + Negate(4) = -1 + -1 + Negate(3) = -1 + -1 + -1 + Negate(2) = -1 + -1 + -1 + -1 + Negate(1) = -1 + -1 + -1 + -1 + -1 + Negate(0) = -5
        Console.WriteLine($"Neg(5) = {RecursionCalculator.Negate(5)}");
        // Negate(-5) = 5 = 1 + Negate(-4) = 1 + 1 + Negate(-3) = 1 + 1 + 1 + Negate(-2) = 1 + 1 + 1 + 1 + Negate(-1) = 1 + 1 + 1 + 1 + 1 + Negate(0) = 5
        Console.WriteLine($"Neg(-5) = {RecursionCalculator.Negate(-5)}");

        // 5 x 4 = 5 + ( 5 x 3 ) = 5 + 5 + ( 5 x 2 ) = 5 + 5 + 5 + ( 5 x 1 ) = 5 + 5 + 5 + 5 + ( 5 x 0 ) = 5 + 5 + 5 + 5 + 0 = 20
        // Multiplication(5, 3) = 15 = 5 + Multiplication(5, 2) = 15 = 5 + 5 + Multiplication(5, 1) = 15 = 5 + 5 + 5 + Multiplication(5, 0) = 15
        Console.WriteLine($"5 * 3 = Multiply(5, 3) = {RecursionCalculator.Multiply(5, 3)}");

        // 5 x -3 = -15 = -1 * ( 5 x (-1 * 3) )
        Console.WriteLine($"5 * -3 = Multiply(5, -3) = {RecursionCalculator.Multiply(5, -3)}");

        // 5 ^ 3 = 5 * ( 5 ^ 2 ) = 5 * 5 * ( 5 ^ 1 ) = 5 * 5 * 5 * ( 5 ^ 0 ) = 5 * 5 * 5 * 1 = 125
        // Power(5, 3) = 125 = 5 * Power(5, 2) = 125 = 5 * 5 * Power(5, 1) = 125 = 5 * 5 * 5 * Power(5, 0) = 125
        Console.WriteLine($"5 ^ 3 = Power(5, 3) = {RecursionCalculator.Power(5, 3)}");
    }

    public static void SumDigitsDemo()
    {
        AppDecorator.Header("Recursion: sum of digits");
        const int number = 20261006;
        Console.WriteLine($"The sum of the digits of {number} is {RecursionCalculator.SumDigits(number)}.");
    }

    public static void TowerOfHanoiDemo()
    {
        AppDecorator.Header("Recursion: Tower of Hanoi");

        const int diskCount = 4;
        Console.WriteLine($"Move {diskCount} disks from A to C:\n");
        TowerOfHanoi.MoveDisks(diskCount, 'A', 'C', 'B', 0);
    }

    public static void DrawerDemo()
    {
        AppDecorator.Header("🎄 PINE");
        int treeHeight = 8;
        int trunkHeight = 2;
        Drawer.DrawPine(treeHeight, trunkHeight);
    }

    static bool IsPalindrom(string text)
    {
        if (text.Length <= 1)
        {
            return true;
        }
        char first = text[0];
        char last = text[text.Length - 1];
        if (first != last)
        {
            return false;
        }
        return IsPalindrom(text.Substring(1, text.Length - 2));
    }

    public static void SierpinskiTriangleDemo( )
    {
        for (int order = 0; order <= 5; order++)
        {
            SierpinskiTriangleDemo(order);
        }
    }

    public static void SierpinskiTriangleDemo( int order )
    {
        AppDecorator.Header("Recursion: Sierpinski triangle (level: " + order + ")");
        int size = 1 << order;
        int width = 2 * size - 1;
        char[,] canvas = new char[size, width];
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < width; column++)
            {
                canvas[row, column] = ' ';
            }
        }
        DrawSierpinskiTriangle(canvas, 0, 0, size);
        for (int row = 0; row < size; row++)
        {
            for (int column = 0; column < width; column++)
            {
                Console.Write(canvas[row, column]);
            }
            Console.WriteLine();
        }
    }

    private static void DrawSierpinskiTriangle(char[,] canvas, int top, int left, int size)
    {
        if (size == 1)
        {
            canvas[top, left] = '*';
            return;
        }
        int halfSize = size / 2;
        DrawSierpinskiTriangle(canvas, top, left + halfSize / 2, halfSize);
        DrawSierpinskiTriangle(canvas, top + halfSize, left, halfSize);
        DrawSierpinskiTriangle(canvas, top + halfSize, left + halfSize, halfSize);
    }

}