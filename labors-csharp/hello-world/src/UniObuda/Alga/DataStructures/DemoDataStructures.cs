namespace HelloWorld.UniObuda.Alga.DataStuctures;

using System;
using System.Collections.Generic;

public static class DemoDataStructures
{

    public static void HelloDemo()
    {
        AppDecorator.Header("Data Stuctures");
        
        IStack<string> stack = new ArrayStack<string>(3);
        stack.Push("Foo");
        stack.Push("Bar");
        Console.WriteLine(stack.Pop() + " --> Bar");
        stack.Push("FooBar");
        Console.WriteLine(stack.Pop() + " --> FooBar");
        Console.WriteLine(stack.Pop() + " --> Foo");
    }

    public static void HelloBrowserHistory()
    {
        AppDecorator.Header("Web browser history - Stack demo");

        const int capacity = 5;
        IStack<string> history = new ArrayStack<string>(capacity);

        int step = 1;
        DrawBrowserHistory(step++, history);
        Visit(step++, history, "uni-obuda.hu");
        Visit(step++, history, "neptun.uni-obuda.hu");
        Visit(step++, history, "testline.hu");
        GoBack(step++, history);
        Visit(step++, history, "moodle.uni-obuda.hu");
        GoBack(step++, history);
        GoBack(step++, history);
        GoBack(step++, history);
    }

    private static void Visit(int step, IStack<string> history, string page)
    {
        Console.WriteLine($"\n╺═[ {step } ]═╸open page (push): {page}");
        history.Push(page);
        DrawBrowserHistory(step, history);
    }

    private static void GoBack(int step, IStack<string> history)
    {
        string pageLeft = history.Pop();
        string currentPage = history.Empty ? "<no page>" : history.Peek();
        Console.WriteLine($"\n╺═[ {step } ]═╸go back to: {currentPage} --> left this page (pop): {pageLeft}");
        DrawBrowserHistory(step, history);
    }

    private static void DrawBrowserHistory(int step, IStack<string> history)
    {
        const string stackIndent = "        ";                                
        Console.WriteLine($"╺═[ {step } ]═╗                              ╔══════╸");
        List<string> pagesFromTop = new List<string>();
        int index = history.Size;
        foreach (string page in history)
        {
            pagesFromTop.Add($"{index--}: {page}");
        }
        for (int slot = pagesFromTop.Count; slot < history.Capacity; slot++)
        {
            DrawStackRow("[ empty space ]", stackIndent, stackIndent);
        }
        for (int pageIndex = 0; pageIndex < pagesFromTop.Count; pageIndex++)
        {
            string rowIndent = pageIndex == 0 ? "  TOP → " : stackIndent;
            DrawStackRow(pagesFromTop[pageIndex], rowIndent, stackIndent);
        }
        Console.WriteLine($"{stackIndent}╚══════════════════════════════╝\n");
    }

    private static void DrawStackRow(string content, string rowIndent, string stackIndent)
    {
        Console.WriteLine($"{stackIndent}╠══════════════════════════════╣");
        Console.WriteLine($"{rowIndent}║ {content.PadRight(28)} ║");
    }

}
