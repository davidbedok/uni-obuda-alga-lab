namespace HelloWorld.UniObuda.Alga.Generic;

using System;
using HelloWorld.UniObuda.Alga.Generic;
using HelloWorld.UniObuda.Alga.Generic.Jobs;

public class DemoGeneric
{
    
    public static void GenericSyntaxDemo()
    {
        AppDecorator.Header("Generic Syntax Demo");

        Console.WriteLine("-- With object arg..");
        Console.WriteLine("Person name: {0}", GenericSample.TellMeYourName(new Person("Doe", "John")));
        Console.WriteLine("Pet name: {0}", GenericSample.TellMeYourName(new Pet("Hercegno")));

        Console.WriteLine("-- With interface arg..");
        Console.WriteLine("Person name: {0}", GenericSample.TellMeYourNameInterface(new Person("Doe", "John")));
        Console.WriteLine("Pet name: {0}", GenericSample.TellMeYourNameInterface(new Pet("Hercegno")));

        Console.WriteLine("-- With generic arg..");
        Console.WriteLine("Person name: {0}", GenericSample.TellMeYourNameGeneric(new Person("Doe", "John")));
        Console.WriteLine("Pet name: {0}", GenericSample.TellMeYourNameGeneric(new Pet("Hercegno")));

        Console.WriteLine("-- With generic arg with constraint..");
        Console.WriteLine("Person name: {0}", GenericSample.TellMeYourNameGenericWithConstraint(new Person("Doe", "John")));
        Console.WriteLine("Pet name: {0}", GenericSample.TellMeYourNameGenericWithConstraint(new Pet("Hercegno")));
    }

    public static void GenericMultiArgsDemo()
    {
        AppDecorator.Header("Generic Multi Args Demo");

        Person anakinSkywalker = new Person("Anakin", "Skywalker");
        Person darthVader = new Person("Darth", "Vader");
        Pet princess = new Pet("Hercegno");
        Pet roland = new Pet("Roli");

        Console.WriteLine("-- With object arg..");
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNames(anakinSkywalker, darthVader));
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNames(darthVader, princess));
       
        Console.WriteLine("-- With interface arg..");
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNamesInterface(anakinSkywalker, darthVader));
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNamesInterface(darthVader, princess));
       
        Console.WriteLine("-- With generic arg..");
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNamesGeneric(anakinSkywalker, darthVader));
        // Console.WriteLine("Names: {0}", GenericSample.TellMeYourNamesGeneric(darthVader, princess));
        Console.WriteLine("Names: {0}", GenericSample.TellMeYourNamesGeneric<IHasName>(darthVader, princess));
    }

    public static void GenericReturnTypeDemo()
    {
        AppDecorator.Header("Generic Return Type Demo");

        Person anakinSkywalker = new Person("Anakin", "Skywalker");
        Person darthVader = new Person("Darth", "Vader");
        Pet princess = new Pet("Hercegno");
        
        Console.WriteLine("-- With object arg..");
        object lastObject = GenericSample.GetLast(anakinSkywalker, princess);
        if ( lastObject is IHasName hasName )
        {
            Console.WriteLine("Last name: {0}", hasName.Name);
        }
        
        Console.WriteLine("-- With generic arg..");
        Person lastPerson = GenericSample.GetLastGeneric<Person>(anakinSkywalker, darthVader);
        Console.WriteLine("Last name: {0}", lastPerson.Name);
    }

    public static void GenericJobDemo()
    {
        AppDecorator.Header("Generic Job Demo");

        Job<IWorker> workerJob = new Job<IWorker>();
        workerJob.AddWorker(new TransactionHandler(1));
        workerJob.AddWorker(new TransactionHandler(2));
        workerJob.AddWorker(new Archiver());
        
        workerJob.ProcessWorkers();

        Job<TransactionHandler> transactionJob = new Job<TransactionHandler>();
        transactionJob.AddWorker(new TransactionHandler(1));
        transactionJob.AddWorker(new TransactionHandler(2));
        transactionJob.AddWorker(new TransactionHandler(3));
        // transactionJob.AddWorker(new Archiver());

        transactionJob.ProcessWorkers();
    }

}
