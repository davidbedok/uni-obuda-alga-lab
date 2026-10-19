namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using System;

public sealed class UnregisteredPurchaseNotifier : ICheckoutCompletedHandler
{
    public void Handle(string addressee)
    {
        Console.WriteLine($"Purchase without registration to {addressee}...");
    }
}
