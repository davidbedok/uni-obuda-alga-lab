using System;
using System.Text;
using System.Linq;

namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

public class LinearEquationIndividual
{

    private readonly double[] genes;

    public double[] Genes => genes;

    public int Dimensions => genes.Length;

    public LinearEquationIndividual(double[] genes)
    {
        this.genes = genes;
        Normalize();
    }

    public double Evaluate(LinearEquation equation)
    {
        double result = 0;
        for (int i = 0; i < this.Dimensions; i++) {
            result += equation.Weights[i] * this.genes[i];
        }
        return result;
    }

    public double Fitness(LinearEquation equation)
    {
        double value = Math.Abs(Evaluate(equation));
        return double.IsFinite(value) ? value : double.PositiveInfinity;
    }

    public double Tolerance(LinearEquation equation, double tolerance)
    {
        return Math.Abs(Fitness(equation) - tolerance);
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

    public string Print(LinearEquation equation, double tolerance )
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < this.Dimensions; i++)
        {
            sb.Append($"{LinearEquationDemo.FormatNumber(equation.Weights[i])} * {LinearEquationDemo.FormatNumber(this.genes[i])}");
            if (i < this.Dimensions - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {LinearEquationDemo.FormatNumber(Evaluate(equation))} --> f: {LinearEquationDemo.FormatNumber(Fitness(equation))} t: {LinearEquationDemo.FormatNumber(Tolerance(equation, tolerance))}");
        return sb.ToString();
    }

    public string PrintVerify( )
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < this.Dimensions; i++)
        {
            sb.Append($"{LinearEquationDemo.FormatNumber(this.genes[i])} * {LinearEquationDemo.FormatNumber(this.genes[i])}");
            if (i < this.Dimensions - 1)
            {
                sb.Append(" + ");
            }
        }
        sb.Append($" = {LinearEquationDemo.FormatNumber(this.Verify())}");
        return sb.ToString();
    }

    public static LinearEquationIndividual Generate(Random random, int dimensions, double minX, double maxX)
    {
        double[] genes = new double[dimensions];
        for (int j = 0; j < dimensions; j++)
        {
            genes[j] = random.NextDouble() * (maxX - minX) + minX;
        }
        return new LinearEquationIndividual(genes);
    }

    // A konstruktor biztosítja, hogy minden egyed egységnyi hosszú legyen.
    private void Normalize()
    {
        double scale = genes.Max(gene => Math.Abs(gene));
        if (scale == 0)
        {
            genes[0] = 1;
            return;
        }
        for (int i = 0; i < genes.Length; i++) genes[i] /= scale;
        double length = Math.Sqrt(genes.Sum(gene => gene * gene));
        for (int i = 0; i < genes.Length; i++) genes[i] /= length;
    }

    public LinearEquationIndividual Reproduce(Random random, LinearEquationIndividual father, EvolutionOptions options)
    {
        double[] childGenes = new double[this.Dimensions];
        for (int i = 0; i < childGenes.Length; i++)
        {
            if (random.NextDouble() < options.ImmigrationRate) {
                childGenes[i] = options.MinX + random.NextDouble() * (options.MaxX - options.MinX);
            }
            else
            {
                // Uniform keresztezés: minden génnél külön választunk szülőt.
                double inherited = random.Next(2) == 0 ? genes[i] : father.genes[i];
                childGenes[i] = inherited + (2 * random.NextDouble() - 1) * options.MutationEpsilon;
            }
        }
        return new LinearEquationIndividual(childGenes);
    }

}
