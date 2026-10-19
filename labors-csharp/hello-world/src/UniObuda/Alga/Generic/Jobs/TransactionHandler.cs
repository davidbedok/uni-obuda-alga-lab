namespace HelloWorld.UniObuda.Alga.Generic.Jobs;

public class TransactionHandler: IWorker
{
    
    private readonly int identifier;

    public TransactionHandler(int identifier)
    {
        this.identifier = identifier;
    }

    public string Process()
    {
        return $"Processing transaction: {this.identifier}...";
    }
    
}