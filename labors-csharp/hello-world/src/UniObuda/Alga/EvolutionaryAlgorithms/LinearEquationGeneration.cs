namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Collections.Generic;
using System.Linq;

public class LinearEquationGeneration
{
    private readonly List<LinearEquationIndividual> population;

    public IReadOnlyList<LinearEquationIndividual> Population => population.AsReadOnly();

    public int Count => population.Count;

    public LinearEquationGeneration()
    {
        this.population = new List<LinearEquationIndividual>();
    }

    public void AddIndividual(LinearEquationIndividual individual)
    {
        population.Add(individual);
    }

    public LinearEquationGeneration CreateNextGeneration(Random random, LinearEquation equation, EvolutionOptions options)
    {
        LinearEquationGeneration result = this.CreateSurvivors(equation, options.ExtinctionRate);
        int numberOfSurvivors = result.Count;
        while (result.Count < Count)
        {
            int motherIndex = random.Next(numberOfSurvivors);
            LinearEquationIndividual mother = result.population[motherIndex];
            int fatherIndex = ( motherIndex + 1 + random.Next(numberOfSurvivors - 1) ) % numberOfSurvivors;
            LinearEquationIndividual father = result.population[fatherIndex];
            result.AddIndividual(mother.Reproduce(random, father, options));
        }
        return result;
    }

    private LinearEquationGeneration CreateSurvivors(LinearEquation equation, double extinctionRate)
    {
        LinearEquationGeneration result = new LinearEquationGeneration();
        LinearEquationGeneration orderedGeneration = this.OrderByFitness(equation);
        int deaths = Math.Clamp((int)Math.Floor(Count * extinctionRate), 1, Count - 1);
        int survivors = Count - deaths;
        for (int i = 0; i < survivors; i++)
        {
            result.AddIndividual(orderedGeneration.Population[i]);
        }
        return result;
    }

    public LinearEquationGeneration OrderByFitness(LinearEquation equation)
    {
        LinearEquationGeneration result = new LinearEquationGeneration();
        foreach (var individual in population.OrderBy(individual => individual.Fitness(equation)))
        {
            result.AddIndividual(individual);
        }
        return result;
    }

    public static LinearEquationGeneration Generate(Random random, int size, int dimensions, double minX, double maxX)
    {
        LinearEquationGeneration result = new();
        for (int i = 0; i < size; i++)
        {
            result.AddIndividual(LinearEquationIndividual.Generate(random, dimensions, minX, maxX));
        }
        return result;
    }
}
