namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;
using System.Collections.Generic;
using System.Linq;

public class Generation
{
    private readonly List<Individual> population;

    public IReadOnlyList<Individual> Population => population.AsReadOnly();

    public int Size => population.Count;

    public Generation()
    {
        this.population = new List<Individual>();
    }

    public void AddIndividual(Individual individual)
    {
        this.population.Add(individual);
    }

    public Generation CreateNextGeneration(Random random, Equation equation, EvolutionOptions options)
    {
        Generation result = this.CreateSurvivors(equation, options.ExtinctionRate);
        List<Individual> imigrants = this.CreateImmigrants(random, equation, options);
        foreach (var imigrant in imigrants)
        {
            result.AddIndividual(imigrant);
        }
        int numberOfSurvivorsAndImigrants = result.Size;
        while (result.Size < Size)
        {
            int motherIndex = random.Next(numberOfSurvivorsAndImigrants);
            Individual mother = result.population[motherIndex];
            int fatherIndex = ( motherIndex + 1 + random.Next(numberOfSurvivorsAndImigrants - 1) ) % numberOfSurvivorsAndImigrants;
            Individual father = result.population[fatherIndex];
            Individual child = mother.Reproduce(random, father, options);
            result.AddIndividual(child);
        }
        return result;
    }

    private Generation CreateSurvivors(Equation equation, double extinctionRate)
    {
        Generation result = new Generation();
        Generation orderedGeneration = this.OrderByFitness(equation);
        int deaths = Math.Clamp((int)Math.Floor(Size * extinctionRate), 1, Size - 1);
        int survivors = Size - deaths;
        for (int i = 0; i < survivors; i++)
        {
            result.AddIndividual(orderedGeneration.Population[i].AsParent());
        }
        return result;
    }

    private List<Individual> CreateImmigrants(Random random, Equation equation, EvolutionOptions options)
    {
        List<Individual> result = new List<Individual>();
        int numberOfImigrants = Convert.ToInt32(Math.Floor(options.ImmigrationRate * Size));
        for (int i = 0; i < numberOfImigrants; i++)
        {
            result.Add(Individual.Generate(random, equation.Dimensions, options.MinGeneValue, options.MaxGeneValue));
        }
        return result;
    }

    public Generation OrderByFitness(Equation equation)
    {
        Generation result = new Generation();
        foreach (var individual in population.OrderBy(individual => individual.Fitness(equation)))
        {
            result.AddIndividual(individual);
        }
        return result;
    }

    public Individual Top(Equation equation)
    {
        return population.OrderBy(individual => individual.Fitness(equation)).First();  
    } 

    public List<Individual> TopN(Equation equation, int numberOfIndividuals)
    {
        return population.OrderBy(individual => individual.Fitness(equation)).Take(numberOfIndividuals).ToList();  
    }
   
    public static Generation Generate(Random random, EvolutionOptions options, int dimensions)
    {
        Generation result = new();
        for (int i = 0; i < options.PopulationSize; i++)
        {
            result.AddIndividual(Individual.Generate(random, dimensions, options.MinGeneValue, options.MaxGeneValue));
        }
        return result;
    }
}
