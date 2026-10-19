namespace HelloWorld.UniObuda.Alga.Functional.Classic;

using System.Text;
using HelloWorld.UniObuda.Alga.Functional;

public sealed class PrettyInvoiceGenerator : IInvoiceGenerator
{
    public string Generate(CheckoutResult result)
    {
        StringBuilder invoice = new StringBuilder();
        invoice.AppendLine("=========== INVOICE ===========");
        invoice.AppendLine($"Customer: {result.Customer}");
        invoice.AppendLine("-------------------------------");
        foreach (var item in result.Items)
        {
            invoice.AppendLine($"{item.Key}\t:\t{item.Value:C}");
        }
        invoice.AppendLine("-------------------------------");
        invoice.AppendLine($"Total: {result.Total:C}");
        return invoice.ToString();
    }
}
