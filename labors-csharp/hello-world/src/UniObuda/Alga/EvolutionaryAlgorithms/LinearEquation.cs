namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Linq;
using System.Text;

// a * A + b * B + c * C = 0
// A² + B² + C² ≈ 1 (of unit length, not a zero vector)
public class LinearEquation
{

    private readonly double[] weights;

    public int Dimensions => weights.Length;

    public double[] Weights => weights;

    public LinearEquation(double[] weights)
    {
        this.weights = weights;
    }

    public LinearEquationIndividual GenerateKnownSolution(Random random)
    {
        ArgumentNullException.ThrowIfNull(random);
        if (weights.Length < 2 || weights.Any(weight => !double.IsFinite(weight)))
        {
            throw new InvalidOperationException("Legalább két véges súly szükséges az egységnyi hosszú megoldás előállításához.");
        }

        // A legnagyobb abszolút súlyhoz tartozó változót számítjuk ki.
        // Így nem osztunk nullával vagy a többinél sokkal kisebb súllyal.
        int pivot = 0;
        for (int i = 1; i < weights.Length; i++)
        {
            if (Math.Abs(weights[i]) > Math.Abs(weights[pivot])) pivot = i;
        }

        double[] known = new double[weights.Length];
        for (int i = 0; i < weights.Length; i++)
        {
            if (i != pivot) known[i] = random.Next(-10, 11) / 10.0;
        }
        // Ne keletkezzen nullvektor akkor sem, ha minden sorsolás nulla.
        if (known.All(value => value == 0)) known[pivot == 0 ? 1 : 0] = 1;

        if (weights[pivot] != 0)
        {
            // Például C = -(a*A + b*B) / c.
            // Tagonként osztunk: a súlyarányok abszolút értéke legfeljebb 1.
            double sum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                if (i != pivot) sum += (weights[i] / weights[pivot]) * known[i];
            }
            known[pivot] = -sum;
        }
        // Csupa nulla súlynál bármely vektor megoldás.
        return new LinearEquationIndividual(known);
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < weights.Length; i++)
        {
            sb.Append($"{LinearEquationDemo.FormatNumber(this.weights[i])} * {GetVariableName(i).PadLeft(LinearEquationDemo.NumberWidth)}");
            if (i < weights.Length - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {LinearEquationDemo.FormatNumber(0)}");
        return sb.ToString();
    }

    // A, B, ..., Z, AA, AB
    private static string GetVariableName(int index)
    {
        string name = "";
        for (int number = index + 1; number > 0; number = (number - 1) / 26)
        {
            name = (char)('A' + (number - 1) % 26) + name;
        }
        return name;
    }

    public static LinearEquation Generate(Random random, int dimensions, int maxWeight)
    {
        double[] weights = new double[dimensions];
        for (int i = 0; i < dimensions; i++)
        {
            int sign = random.Next(2) == 0 ? -1 : 1;
            weights[i] = random.Next(1, maxWeight + 1) / 10.0 * sign;
        }
        return new LinearEquation(weights);
    }

}
