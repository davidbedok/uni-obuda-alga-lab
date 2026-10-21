using System;
using System.Text;
using System.Linq;

namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

public class Individual
{

    private readonly IndividualType type;

    private readonly double[] genes;

    public double[] Genes => this.genes;
    
    public int Dimensions => genes.Length;

    public Individual(IndividualType type, double[] genes)
    {
        this.type = type;
        this.genes = genes;
    }

    public double Fitness(Equation equation)
    {
        return Math.Abs(equation.Evaluate(this));
    }

    public bool IsWithinTolerance(Equation equation, double tolerance)
    {
        return Fitness(equation) <= tolerance;
    }

    public double Verify()
    {
        double result = 0;
        for (int i = 0; i < this.Dimensions; i++)
        {
            result += this.genes[i] * this.genes[i];
        }
        return result;
    }

    public string Print(Equation equation, double tolerance )
    {
        StringBuilder sb = new StringBuilder();
        sb.Append($"[{this.type, 9}] ");
        for (int i = 0; i < this.Dimensions; i++)
        {
            sb.Append($"{GenericAlgorithmDemo.FormatNumber(equation.Weights[i])} * {GenericAlgorithmDemo.FormatNumber(this.genes[i])}^{i+1}");
            if (i < this.Dimensions - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {GenericAlgorithmDemo.FormatNumber(equation.Evaluate(this))}");
        return sb.ToString();
    }

    public string PrintVerify( )
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < this.Dimensions; i++)
        {
            sb.Append($"{GenericAlgorithmDemo.FormatNumber(this.genes[i])} * {GenericAlgorithmDemo.FormatNumber(this.genes[i])}");
            if (i < this.Dimensions - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {GenericAlgorithmDemo.FormatNumber(this.Verify())}");
        return sb.ToString();
    }

    public static Individual Generate(Random random, int dimensions, double minX, double maxX)
    {
        double[] genes = new double[dimensions];
        for (int j = 0; j < dimensions; j++)
        {
            genes[j] = random.NextDouble() * (maxX - minX) + minX;
        }
        return new Individual(IndividualType.Immigrant, genes);
    }

    public Individual Reproduce(Random random, Individual father, EvolutionOptions options)
    {
        double[] childGenes = new double[this.Dimensions];
        for (int i = 0; i < childGenes.Length; i++)
        {
            double inherited = random.Next(2) == 0 ? this.genes[i] : father.genes[i];
            childGenes[i] = inherited + (2 * random.NextDouble() - 1) * options.MutationEpsilon;
        }
        return new Individual(IndividualType.Child, childGenes);
    }

    public Individual AsParent()
    {
        return new Individual(IndividualType.Parent, this.genes);
    }

}
