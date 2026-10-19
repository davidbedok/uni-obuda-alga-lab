namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Linq;

public static class LinearEquationDemo
{

    private static int numberWidth = 10;

    // A tizedesjegyek száma a mezőszélességből következik: X - 4.
    public static int NumberWidth
    {
        get => numberWidth;
        set
        {
            if (value < 4) throw new ArgumentOutOfRangeException(nameof(value), "A mezőszélesség legalább 4 legyen.");
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
            MaxGenerations = 200,
            ExtinctionRate = 0.5,
            MutationEpsilon = 0.05,
            ImmigrationRate = 0.1,
            MinX = -1,
            MaxX = 1,
            Tolerance = 0.001
        };

        Console.WriteLine("Generated linear equations:");
        LinearEquation equation = LinearEquation.Generate(new Random(), 3, 40);
        Console.WriteLine(equation);
        Console.WriteLine("\nSome known solutions:");
        for ( int i = 0; i < 3; i++)
        {
            LinearEquationIndividual knownSolution = equation.GenerateKnownSolution(new Random());
            Console.WriteLine(knownSolution.Print(equation, options.Tolerance));
            Console.WriteLine(knownSolution.PrintVerify());
        }
        
        Random random = new Random(42);
        LinearEquationSolver solver = new LinearEquationSolver(options, previous => previous.CreateNextGeneration(random, equation, options));

        Console.WriteLine("\nEvolution:");
        LinearEquationIndividual solution = solver.Solve(random, equation, (generationIndex, generation) =>
        {
            Console.WriteLine($"\n#[{generationIndex}] Generation. Best fitness: {FormatNumber(generation.Population[0].Fitness(equation))}");
            foreach (var individual in generation.Population.Take(3)) {
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
