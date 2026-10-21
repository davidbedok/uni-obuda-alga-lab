namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Linq;
using System.Text;

// w1 * A^1 + w2 * B^2 + w3 * C^3 + ... = 0
public class Equation
{

    private readonly double[] weights;

    public int Dimensions => this.weights.Length;

    public double[] Weights => this.weights;

    public double this[int index]
    {
        get
        {
            return this.weights[index];
        }
    }

    private Equation(double[] weights)
    {
        this.weights = weights;
    }

    public double Evaluate(Individual individual)
    {
        double result = 0;
        for (int i = 0; i < this.Dimensions; i++) {
            result += this.weights[i] * Math.Pow(individual.Genes[i], i + 1);
        }
        return result;
    }

    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("[---------] ");
        for (int i = 0; i < weights.Length; i++)
        {
            sb.Append($"{GenericAlgorithmDemo.FormatNumber(this.weights[i])} * {GetVariableName(i).PadLeft(GenericAlgorithmDemo.NumberWidth)}^{i+1}");
            if (i < weights.Length - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {GenericAlgorithmDemo.FormatNumber(0)}");
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

    public static Equation Generate(Random random, int dimensions, int maxWeight)
    {
        double[] weights = new double[dimensions];
        for (int i = 0; i < dimensions; i++)
        {
            int sign = random.Next(2) == 0 ? -1 : 1;
            weights[i] = random.NextDouble() * maxWeight * sign;
        }
        return new Equation(weights);
    }

}
