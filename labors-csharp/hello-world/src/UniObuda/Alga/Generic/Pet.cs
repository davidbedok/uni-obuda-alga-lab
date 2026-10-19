namespace HelloWorld.UniObuda.Alga.Generic;

public class Pet : IHasName
{
    
    private readonly string name;

    public string Name
    {
        get { return name; }
    }

    public Pet(string name)
    {
        this.name = name;
    }

}