namespace HelloWorld.UniObuda.Alga.DataStuctures;

using System;
using System.Collections;
using System.Collections.Generic;

public class ArrayStackEnumerator<T> : IEnumerator<T>
{
    private readonly T[] items;
    private readonly int topIndex;
    private int currentIndex;

    public ArrayStackEnumerator(T[] items, int topIndex)
    {
        this.items = items;
        this.topIndex = topIndex;
        this.currentIndex = topIndex + 1;
    }

    public T Current
    {
        get
        {
            if (this.currentIndex < 0 || this.currentIndex > this.topIndex)
            {
                throw new InvalidOperationException("A bejáró nem mutat érvényes elemre.");
            }

            return this.items[this.currentIndex];
        }
    }

    object IEnumerator.Current => this.Current!;

    public bool MoveNext()
    {
        if (this.currentIndex <= 0)
        {
            this.currentIndex = -1;
            return false;
        }

        this.currentIndex--;
        return true;
    }

    public void Reset()
    {
        this.currentIndex = this.topIndex + 1;
    }

    public void Dispose()
    {
    }
}
