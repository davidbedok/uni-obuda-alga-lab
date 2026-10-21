namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;

public class EquationSolver
{
    private readonly EvolutionOptions options;
    private readonly Func<Generation, Generation> populationGenerator;

    public EquationSolver(EvolutionOptions options, Func<Generation, Generation> populationGenerator)
    {
        this.options = options;
        this.populationGenerator = populationGenerator;
    }

    public Individual Solve(Random random, Equation equation, Action<int, Generation>? onGeneration = null)
    {
        Generation current = Generation.Generate(random, options, equation.Dimensions);
        int generationCounter = 0;
        onGeneration?.Invoke(generationCounter, current);
        while (generationCounter < options.MaxGenerations && !current.Top(equation).IsWithinTolerance(equation, options.Tolerance))
        {
            current = populationGenerator(current);
            onGeneration?.Invoke(++generationCounter, current);
        }
        return current.Top(equation);
    }
}
