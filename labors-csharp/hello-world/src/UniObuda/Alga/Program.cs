namespace HelloWorld.UniObuda.Alga;

using HelloWorld.UniObuda.Alga.DataStuctures;
using HelloWorld.UniObuda.Alga.Functional;
using HelloWorld.UniObuda.Alga.Generic;
using HelloWorld.UniObuda.Alga.Recursion;
using HelloWorld.UniObuda.Alga.EvolutionaryAlgorithms;

public class Program
{
    public static void Main(string[] args)
    {
        // GenericDemo();
        // FunctionalDemo();
        // DataStucturesDemo();
        // RecursionDemo();
        EvolutionDemo();
    }

    private static void GenericDemo()
    {
        DemoGeneric.GenericSyntaxDemo();
        DemoGeneric.GenericMultiArgsDemo();
        DemoGeneric.GenericReturnTypeDemo();
        DemoGeneric.GenericJobDemo();
    }

    private static void FunctionalDemo()
    {
        DemoFunctional.CallbackWithInterfacesDemo();
        DemoFunctional.CallbackWithDelegatesDemo();
    }

    private static void DataStucturesDemo()
    {
        DemoDataStructures.HelloBrowserHistory();
    }

    private static void RecursionDemo()
    {
        DemoRecursion.CountDownDemo();
        DemoRecursion.CalculationDemo();
        DemoRecursion.SumDigitsDemo();
        DemoRecursion.FactorialDemo();
        DemoRecursion.FibonacciDemo();
        DemoRecursion.DrawerDemo();
        // DemoRecursion.TowerOfHanoiDemo();
        DemoRecursion.SierpinskiTriangleDemo();
    }

    private static void EvolutionDemo()
    {
        LinearEquationDemo.ExecuteDemo();
    }

}
