using System.Collections;
using System.Collections.Generic;

namespace HelloWorld.UniObuda.Alga.DataStuctures;

public class ArrayStack<T> : IStack<T>
{

    private T[] items;

    private int index;

    public bool Empty => index == -1;

    public bool Full => index == this.Capacity - 1;

    public int Capacity => this.items.Length;

    public int Size => this.index + 1;

    public ArrayStack( int capacity )
    {
        this.items = new T[capacity];
        this.index = -1;
    }

    public void Push(T value)
    {
        if ( !this.Full )
        {
            this.items[++this.index] = value;
        } else
        {
            throw new NoMoreSpaceException();
        }
    }

    public T Pop()
    {
        if ( !this.Empty )
        {
            return this.items[this.index--];
        } else
        {
            throw new NoItemException();
        }
    }

    public T Peek()
    {
        if ( !this.Empty )
        {
            return this.items[this.index];
        } else
        {
            throw new NoItemException();
        }
    }

    public IEnumerator<T> GetEnumerator() => new ArrayStackEnumerator<T>(this.items, this.index);

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

}
