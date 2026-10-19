namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Linq;

public class LinearEquationSolver
{
    private readonly EvolutionOptions options;
    private readonly Func<LinearEquationGeneration, LinearEquationGeneration> populationGenerator;

    public LinearEquationSolver(EvolutionOptions options, Func<LinearEquationGeneration, LinearEquationGeneration> populationGenerator)
    {
        this.options = options;
        this.populationGenerator = populationGenerator;
    }

    public LinearEquationIndividual Solve(Random random, LinearEquation equation,
        Action<int, LinearEquationGeneration>? onGeneration = null)
    {
        LinearEquationGeneration current = LinearEquationGeneration.Generate(random,
            options.PopulationSize, equation.Dimensions, options.MinX, options.MaxX);

        LinearEquationIndividual? best = null;
        double bestFitness = double.PositiveInfinity;

        for (int generationCounter = 0; ; generationCounter++)
        {
            current = current.OrderByFitness(equation);
            onGeneration?.Invoke(generationCounter, current);

            LinearEquationIndividual candidate = current.Population[0];
            double candidateFitness = candidate.Fitness(equation);
            if (best == null || candidateFitness < bestFitness)
            {
                best = candidate;
                bestFitness = candidateFitness;
            }
            if (bestFitness <= options.Tolerance || generationCounter == options.MaxGenerations)
            {
                break;
            }
            
            current = populationGenerator(current);
        }
        return best;
        
    }
}
