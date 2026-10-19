namespace HelloWorld.UniObuda.Alga.Functional;

using System.Collections.Generic;
using System.Linq;

public class CheckoutResult
{

    private readonly string customer;

    private readonly Dictionary<string, decimal> items;

    public Dictionary<string, decimal> Items => items;

    public string Customer { get { return this.customer; } }

    public decimal Total
    {
        get
        {
            return this.items.Sum(kvp => kvp.Value);
        }
    }

    public CheckoutResult(string customer)
    {
        this.customer = customer;
        this.items = new Dictionary<string, decimal>();
    }

    public void AddItem(string name, decimal price)
    {
        this.items.Add(name, price);
    }

}
