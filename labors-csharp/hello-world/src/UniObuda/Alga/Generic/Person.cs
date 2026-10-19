namespace HelloWorld.UniObuda.Alga.Generic;

public class Person : IHasName
{
    
    private readonly string familyName;

    private readonly string givenName;

    public string Name
    {
        get { return familyName + " " + givenName; }
    }

    public Person(string familyName, string givenName)
    {
        this.familyName = familyName;
        this.givenName = givenName;
    }

}