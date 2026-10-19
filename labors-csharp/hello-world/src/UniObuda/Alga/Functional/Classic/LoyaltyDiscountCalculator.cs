namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using HelloWorld.UniObuda.Alga.Functional;

public sealed class LoyaltyDiscountCalculator : IDiscountCalculator
{
    public decimal Calculate(Product product)
    {
        return product.Total * 0.9m;
    }
}
