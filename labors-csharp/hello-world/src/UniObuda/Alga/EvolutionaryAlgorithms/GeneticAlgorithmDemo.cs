namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;

public static class GenericAlgorithmDemo
{

    private static int numberWidth = 8;

    public static int NumberWidth
    {
        get => numberWidth;
        set
        {
            numberWidth = value;
        }
    }

    public static void ExecuteDemo(int numberWidth = 8)
    {
        NumberWidth = numberWidth;
        AppDecorator.Header("Linear equation solver with Evolutionary Algorithm");

        EvolutionOptions options = new()
        {
            PopulationSize = 20,
            MaxGenerations = 100,
            ExtinctionRate = 0.5,
            MutationEpsilon = 0.05,
            ImmigrationRate = 0.1,
            MinGeneValue = -5,
            MaxGeneValue = 5,
            Tolerance = 0.005
        };

        Console.WriteLine("Generated equations (w1 * A^1 + w2 * B^2 + w3 * C^3 + ... = 0):");
        Equation equation = Equation.Generate(new Random(), 3, 40);
        Console.WriteLine(equation);
        Console.WriteLine("\nSome known solutions:");
        
        Random random = new Random(42);
        EquationSolver solver = new EquationSolver(options, previous => previous.CreateNextGeneration(random, equation, options));

        Console.WriteLine("\nEvolution:");
        Individual solution = solver.Solve(random, equation, (generationIndex, generation) =>
        {
            Console.WriteLine($"\n#[{generationIndex}] Generation");
            foreach (var individual in generation.TopN(equation, 20)) {
                Console.WriteLine(individual.Print(equation, options.Tolerance));
            }
        });

        Console.WriteLine("\nBest solution:");
        Console.WriteLine(solution.Print(equation, options.Tolerance));
    }

    public static string FormatNumber(double value)
    {
        int precision = NumberWidth - 4;
        string format = precision == 0 ? "0" : "0." + new string('#', precision);
        return value.ToString($"{format};-{format};0").PadLeft(NumberWidth);
    }

}
