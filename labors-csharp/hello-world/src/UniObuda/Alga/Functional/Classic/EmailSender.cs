namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using System;

public sealed class EmailSender : ICheckoutCompletedHandler
{
    public void Handle(string addressee)
    {
        Console.WriteLine($"Email sent to {addressee}...");
    }
}
