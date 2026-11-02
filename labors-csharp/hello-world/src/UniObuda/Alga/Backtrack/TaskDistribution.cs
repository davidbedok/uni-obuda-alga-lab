namespace HelloWorld.UniObuda.Alga.Backtrack;

using System;
using System.Collections.Generic;

public class TaskDistribution
{

    private List<TaskPlan> tasks;

    private uint Levels => (uint)this.tasks.Count;

    private string[][] Possibilities
    {
        get
        {
            string[][] result = new string[this.Levels][];
            for (int i = 0; i < this.Levels; i++)
            {
                TaskPlan task = this.tasks[i];
                result[i] = task.Applicants.ToArray();
            }
            return result;
        }
    }

    public TaskDistribution()
    {
        this.tasks = new List<TaskPlan>();
    }

    public void AddTask(string task, params string[] applicants)
    {
        TaskPlan plan = new TaskPlan(task);
        foreach (string applicant in applicants)
        {
            plan.AddApplicant(applicant);
        }
        this.tasks.Add(plan);
    }

    private bool IsValidSolution(uint level, string solution)
    {
        return true;
    }

    private bool IsValidSolutionWithHistory(uint level, string solution, string[] partialSolutions)
    {
        bool result = true;
        for (int i = 0; i < level; i++)
        {
            if (partialSolutions[i].Equals(solution))
            {
                result = false;
                break;
            }
        }
        return result;
    }

    private int Fitness(string[] solutions)
    {
        return 1;
    }

    public List<TaskAssignment> Solve()
    {
        BacktrackSolver<string> solver = new BacktrackSolver<string>(this.Possibilities, this.IsValidSolution, this.IsValidSolutionWithHistory, this.Fitness);
        return this.GetAssignmentsFromSolution(solver.Solve());
    }

    private List<TaskAssignment> GetAssignmentsFromSolution(string[] solution)
    {
        List<TaskAssignment> result = new List<TaskAssignment>();
        for (int i = 0; i < solution.Length; i++)
        {
            result.Add(new TaskAssignment(this.tasks[i].Task, solution[i]));
        }
        return result;
    }

    public override string ToString()
    {
        string result = "";
        foreach (TaskPlan task in this.tasks)
        {
            result += $"{task}\n";
        }
        return result;
    }

}