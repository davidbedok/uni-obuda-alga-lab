namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using HelloWorld.UniObuda.Alga.Functional;

// A három interface a Functional változat három delegált paraméterének felel meg.
public interface IDiscountCalculator
{
    decimal Calculate(Product product);
}
