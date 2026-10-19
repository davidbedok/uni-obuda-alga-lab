namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using HelloWorld.UniObuda.Alga.Functional;

public interface IInvoiceGenerator
{
    string Generate(CheckoutResult result);
}
