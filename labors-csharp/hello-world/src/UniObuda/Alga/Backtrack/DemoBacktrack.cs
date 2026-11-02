namespace HelloWorld.UniObuda.Alga.Backtrack;

using System;
using System.Collections.Generic;

public class DemoBacktrack
{

    public static void BackTrackRun()
    {
        AppDecorator.Header("Backtrack - Task Distribution");

        TaskDistribution taskDistribution = new TaskDistribution();
        taskDistribution.AddTask("Planning", "Miklos", "Klaudia");
        taskDistribution.AddTask("Direction", "Zsolt", "Miklos");
        taskDistribution.AddTask("Procurement", "Andras");
        taskDistribution.AddTask("Inspection", "Andras", "Pal", "Zsolt");
        taskDistribution.AddTask("Approval", "Andras", "Geza");
        taskDistribution.AddTask("Evaluation", "Geza", "Miklos");
        
        Console.WriteLine("Tasks and applicants:");
        Console.WriteLine(taskDistribution);

        List<TaskAssignment> solution = taskDistribution.Solve();
        Console.WriteLine("Optimal solution:");
        foreach (var assignment in solution)
        {
            Console.WriteLine($"{assignment.Task}: {assignment.Applicant}");
        }
    }

}