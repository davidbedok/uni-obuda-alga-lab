namespace HelloWorld.UniObuda.Alga.Functional;

public class Product
{

    private readonly string name;

    private readonly int quantity;
    private readonly decimal price;

    public string Name { get { return this.name; } }
    public int Quantity { get { return this.quantity; } }
    public decimal Price { get { return this.price; } }
    public decimal Total => this.quantity * this.price;

    public Product(string name, int quantity, decimal price)
    {
        this.name = name;
        this.quantity = quantity;
        this.price = price;
    }
}