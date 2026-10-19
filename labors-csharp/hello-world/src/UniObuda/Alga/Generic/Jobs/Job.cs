namespace HelloWorld.UniObuda.Alga.Generic.Jobs;

using System;
using System.Collections.Generic;

public class Job<T> where T : IWorker
{
    private readonly List<T> workers;

    public Job()
    {
        this.workers = new List<T>();
    }

    public void AddWorker(T worker)
    {
        this.workers.Add(worker);
    }

    public void ProcessWorkers()
    {
        foreach (T worker in this.workers)
        {
            Console.WriteLine(worker.Process());
        }
    }

}