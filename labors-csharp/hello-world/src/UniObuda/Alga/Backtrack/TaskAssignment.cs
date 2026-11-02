namespace HelloWorld.UniObuda.Alga.Backtrack;

public class TaskAssignment
{

    private string task;

    private string applicant;

    public string Task { 
        get => this.task; 
    }

    public string Applicant { 
        get => this.applicant; 
    }

    public TaskAssignment(string task, string applicant)
    {
        this.task = task;
        this.applicant = applicant;
    }

}