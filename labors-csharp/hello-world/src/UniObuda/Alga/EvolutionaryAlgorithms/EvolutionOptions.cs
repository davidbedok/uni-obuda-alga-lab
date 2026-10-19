namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

using System;

public sealed class EvolutionOptions
{
    public int PopulationSize { get; init; } = 100;
    public int MaxGenerations { get; init; } = 500;
    public double ExtinctionRate { get; init; } = 0.5;
    public double MutationEpsilon { get; init; } = 0.05;
    public double ImmigrationRate { get; init; } = 0.1;
    public double MinX { get; init; } = -5;
    public double MaxX { get; init; } = 5;
    public double Tolerance { get; init; } = 0.001;

}
