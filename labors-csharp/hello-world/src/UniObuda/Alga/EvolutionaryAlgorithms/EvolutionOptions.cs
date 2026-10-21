namespace HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

public sealed class EvolutionOptions
{
    public int PopulationSize { get; init; } = 100;
    public int MaxGenerations { get; init; } = 500;
    public double ExtinctionRate { get; init; } = 0.5;
    public double MutationEpsilon { get; init; } = 0.05;
    public double ImmigrationRate { get; init; } = 0.1;
    public double MinGeneValue { get; init; } = -5;
    public double MaxGeneValue { get; init; } = 5;
    public double Tolerance { get; init; } = 0.001;

}
