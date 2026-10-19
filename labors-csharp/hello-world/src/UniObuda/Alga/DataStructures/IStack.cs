using System.Collections.Generic;

namespace HelloWorld.UniObuda.Alga.DataStuctures;

public interface IStack<T> : IEnumerable<T>
{
    bool Empty { get; }

    bool Full { get; }

    int Size { get; }

    int Capacity { get; }

    void Push(T value);
    T Pop();
    T Peek();
}
