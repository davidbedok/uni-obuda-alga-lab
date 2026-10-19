namespace HelloWorld.UniObuda.Alga.Functional;

using System;
using System.Collections.Generic;
using System.Linq;
using HelloWorld.UniObuda.Alga.Functional.Classic;

public class ShoppingCart
{
    private readonly string customer;
    private readonly List<Product> items;

    public string Customer => customer;
    public decimal Subtotal => items.Sum(item => item.Price * item.Quantity);

    public ShoppingCart(string customer)
    {
        this.customer = customer;
        this.items = new List<Product>();
    }

    public void AddItem(Product product)
    {
        items.Add(product);
    }

    public void AddItem(string name, int quantity, decimal price)
    {
        this.AddItem(new Product(name, quantity, price));
    }

    public string CheckoutWithIntefaces(
        IDiscountCalculator discountCalculator,
        ICheckoutCompletedHandler onCheckoutCompleted,
        IInvoiceGenerator invoiceGenerator)
    {
        CheckoutResult result = new CheckoutResult(customer);
        foreach (Product item in items)
        {
            result.AddItem(item.Name, discountCalculator.Calculate(item));
        }

        onCheckoutCompleted.Handle(customer);
        return invoiceGenerator.Generate(result);
    }
    public string Checkout(
        Func<Product, decimal> discountCalculator,
        Action<string> onCheckoutCompleted,
        Func<CheckoutResult, string> invoiceGenerator
    )
    {
        CheckoutResult result = new CheckoutResult(this.customer);
        foreach (Product item in items)
        {
            result.AddItem(item.Name, discountCalculator(item));
        }
        onCheckoutCompleted(this.customer);
        return invoiceGenerator(result);
    }

}