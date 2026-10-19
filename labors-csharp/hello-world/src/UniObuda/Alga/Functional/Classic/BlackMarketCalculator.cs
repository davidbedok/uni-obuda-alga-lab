namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using System;
using HelloWorld.UniObuda.Alga.Functional;

public sealed class BlackMarketCalculator : IDiscountCalculator
{
    private readonly Random random = new Random();

    public decimal Calculate(Product product)
    {
        return product.Total * (decimal)random.NextDouble();
    }
}
