namespace HelloWorld.UniObuda.Alga.Generic;

public class GenericSample
{

    public static string TellMeYourName(object input)
    {
        string result = "";
        if (input is Person person)
        {
            result = person.Name;
        }
        else if (input is Pet pet)
        {
            result = pet.Name;
        }
        return result;
    }

    public static string TellMeYourNameInterface(IHasName input)
    {
        return input.Name;
    }

    public static string TellMeYourNameGeneric<T>(T input)
    {
        string result = "";
        if (input is IHasName hasName)
        {
            result = hasName.Name;
        }
        return result;
    }

    public static string TellMeYourNameGenericWithConstraint<T>(T input) where T : IHasName
    {
        return input.Name;
    }

    public static string TellMeYourNames(object input1, object input2)
    {
        return TellMeYourName(input1) + " and " + TellMeYourName(input2);
    }

    public static string TellMeYourNamesInterface(IHasName input1, IHasName input2)
    {
        return TellMeYourNameInterface(input1) + " and " + TellMeYourNameInterface(input2);
    }

    public static string TellMeYourNamesGeneric<T>(T input1, T input2)
    {
        return TellMeYourNameGeneric(input1) + " and " + TellMeYourNameGeneric(input2);
    }

    public static object GetLast(params object[] items)
    {
        return items[items.Length - 1];
    }

    public static T GetLastGeneric<T>(params T[] items)
    {
        return items[items.Length - 1];
    }

}