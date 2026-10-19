using System;

namespace HelloWorld.UniObuda.Alga.Recursion;

public class TowerOfHanoi
{

    public static void MoveDisks(int count, char source, char target, char spare, int depth)
    {
        if (count == 0)
        {
            return;
        }

        MoveDisks(count - 1, source, spare, target, depth + 1);
        Console.WriteLine($"{new string(' ', depth * 2)}Move disk {count}: {source} -> {target}");
        MoveDisks(count - 1, spare, target, source, depth + 1);
    }

}