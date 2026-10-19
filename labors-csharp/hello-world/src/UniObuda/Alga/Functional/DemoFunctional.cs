namespace HelloWorld.UniObuda.Alga.Functional;

using System;
using System.Text;
using HelloWorld.UniObuda.Alga.Functional.Classic;

public static class DemoFunctional
{

    public static void CallbackWithInterfacesDemo()
    {
        AppDecorator.Header("Callback With Interfaces Demo");

        ShoppingCart cart = new ShoppingCart("Anakin Skywalker");
        cart.AddItem("Lightsaber", 1, 1000m);
        cart.AddItem("Jedi Robe", 2, 200m);

        IDiscountCalculator loyaltyDiscount = new LoyaltyDiscountCalculator();
        ICheckoutCompletedHandler emailSender = new EmailSender();
        IInvoiceGenerator invoiceGenerator = new PrettyInvoiceGenerator();
        Console.WriteLine(cart.CheckoutWithIntefaces(loyaltyDiscount, emailSender, invoiceGenerator));

        IDiscountCalculator blackMarketCalculator = new BlackMarketCalculator();
        ICheckoutCompletedHandler unregisteredNotifier = new UnregisteredPurchaseNotifier();
        Console.WriteLine(cart.CheckoutWithIntefaces(blackMarketCalculator, unregisteredNotifier, invoiceGenerator));
    }

    public static void CallbackWithDelegatesDemo()
    {
        AppDecorator.Header("Callback With Delegates Demo");

        ShoppingCart cart = new ShoppingCart("Anakin Skywalker");
        cart.AddItem("Lightsaber", 1, 1000m);
        cart.AddItem("Jedi Robe", 2, 200m);

        Func<Product, decimal> loyaltyDiscount = product => product.Total * 0.9m;
        Console.WriteLine(cart.Checkout(loyaltyDiscount, SendEmail, PrettyInvoiceGenerator));

        Random random = new Random();

        Func<Product, decimal> blackMarketCalculator = product => product.Total * (decimal)random.NextDouble();
        Console.WriteLine(cart.Checkout(blackMarketCalculator, Nope, PrettyInvoiceGenerator));
    }

    private static void SendEmail(string addressee)
    {
        Console.WriteLine($"Email sent to {addressee}...");
    }

    private static void Nope(string addressee)
    {
        Console.WriteLine($"Purchase without registration to {addressee}...");
    }

    public static string PrettyInvoiceGenerator(CheckoutResult result)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("=========== INVOICE ===========");
        sb.AppendLine($"Customer: {result.Customer}");
        sb.AppendLine("-------------------------------");
        foreach (var item in result.Items)
        {
            sb.AppendLine($"{item.Key}\t:\t{item.Value:C}");
        }
        sb.AppendLine("-------------------------------");
        sb.AppendLine($"Total: {result.Total:C}");
        return sb.ToString();
    }

}
