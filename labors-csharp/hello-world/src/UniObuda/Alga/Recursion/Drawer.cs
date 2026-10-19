namespace HelloWorld.UniObuda.Alga.Recursion;

using System;

public class Drawer
{

    public static void DrawPine(int treeHeight, int trunkHeight)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Drawer.DrawGreenery(treeHeight, 1);

        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Drawer.DrawTrunk(trunkHeight, treeHeight - 2);

        Console.ResetColor();
    }

    public static void DrawGreenery(int remainingHeight, int starCount)
    {
        if (remainingHeight != 0) {;
            string spaces = new string(' ', remainingHeight - 1);
            string stars = new string('*', starCount);
            Console.WriteLine(spaces + stars);
            DrawGreenery(remainingHeight - 1, starCount + 2);
        }
    }

    public static void DrawTrunk(int remainingHeight, int spaces)
    {
        if (remainingHeight != 0) {
            string spacesString = new string(' ', spaces);
            Console.WriteLine(spacesString + "|||");
            DrawTrunk(remainingHeight - 1, spaces);
        }
    }

}