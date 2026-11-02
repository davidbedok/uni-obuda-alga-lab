namespace HelloWorld.UniObuda.Alga.Backtrack;

using System.Collections.Generic;

public class TaskPlan
{

    private string task;

    private List<string> applicants;

    public string Task => this.task;

    public List<string> Applicants => this.applicants;

    public TaskPlan(string task)
    {
        this.task = task;
        this.applicants = new List<string>();
    }

    public void AddApplicant(string applicant)
    {
        this.applicants.Add(applicant);
    }

    public override string ToString()
    {
        return $"{this.task}: {string.Join(", ", this.applicants)}";
    }

}