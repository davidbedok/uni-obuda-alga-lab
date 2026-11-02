namespace HelloWorld.UniObuda.Alga.Backtrack;

using System;

public class BacktrackSolver<T>
{

    private readonly T[][] possibilities;

    private readonly Func<uint, T, bool> isValidSolution;

    private readonly Func<uint, T, T[], bool> isValidSolutionWithHistory;

    private readonly Func<T[], int> fitness;

    private uint Levels => (uint)possibilities.Length;

    public BacktrackSolver(T[][] possibilities, Func<uint, T, bool> isValidSolution, Func<uint, T, T[], bool> isValidSolutionWithHistory, Func<T[], int> fitness)
    {
        this.possibilities = possibilities;
        this.isValidSolution = isValidSolution;
        this.isValidSolutionWithHistory = isValidSolutionWithHistory;
        this.fitness = fitness;
    }

    public T[] Solve()
    {
        T[] partialSolution = new T[this.Levels];
        return this.Solve(0, partialSolution, null) ?? throw new InvalidOperationException("No valid solution exists.");
    }

    private T[]? Solve(uint level, T[] partialSolution, T[]? optimalSolution)
    {
        if (level == this.Levels)
        {
            if (optimalSolution == null || this.fitness(partialSolution) > this.fitness(optimalSolution))
            {
                optimalSolution = this.CreateSolution(partialSolution);
            }
        }
        else
        {
            for ( int i = 0; i < this.GetNumberOfPossibleSelections(level); i++ )
            {
                T candidate = this.GetCandidate(level, i);
                if ( this.isValidSolution(level, candidate) )
                {
                    if ( this.isValidSolutionWithHistory(level, candidate, partialSolution) )
                    {
                        partialSolution[level] = candidate;
                        optimalSolution = this.Solve(level + 1, partialSolution, optimalSolution);
                    }
                }
            }
        }
        return optimalSolution;
    }

    public T[] SolveRef()
    {
        T[] partialSolution = new T[this.Levels];
        bool foundSolution = this.Levels == 0;
        T[] optimalSolution = new T[this.Levels];
        if (this.Levels > 0)
        {
            this.SolveRef(0, ref partialSolution, ref foundSolution, ref optimalSolution);
        }
        if (!foundSolution)
        {
            throw new InvalidOperationException("No valid solution exists.");
        }
        return optimalSolution;
    }

    private void SolveRef(uint level, ref T[] partialSolution, ref bool foundSolution, ref T[] optimalSolution)
    {
        for ( int i = 0; i < this.GetNumberOfPossibleSelections(level); i++ )
        {
            T candidate = this.GetCandidate(level, i);
            if ( this.isValidSolution(level, candidate) )
            {
                if ( this.isValidSolutionWithHistory(level, candidate, partialSolution) )
                {
                    partialSolution[level] = candidate;
                    if ( level == this.Levels - 1 )
                    {
                        if ( !foundSolution || this.fitness(partialSolution) > this.fitness(optimalSolution) )
                        {
                            optimalSolution = this.CreateSolution(partialSolution);
                        }
                        foundSolution = true;
                    }
                    else
                    {
                        this.SolveRef(level + 1, ref partialSolution, ref foundSolution, ref optimalSolution);
                    }
                }
            }
        }
    }

    private uint GetNumberOfPossibleSelections(uint level)
    {
        return (uint)this.possibilities[level].Length;
    }

    private T GetCandidate(uint level, int index)
    {
        return this.possibilities[level][index];
    }

    private T[] CreateSolution( T[] solution )
    {
        T[] result = new T[this.Levels];
        for ( int i = 0; i < this.Levels; i++ )
        {
            result[i] = solution[i];
        }
        return result;
    }
    
}
