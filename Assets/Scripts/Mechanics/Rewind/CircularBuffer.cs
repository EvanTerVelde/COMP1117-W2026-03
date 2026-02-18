using System.Collections.Generic;

public class CircularBuffer<T> //generic circular buffer
{
    // collection itself
    private List<T> buffer;
    // Capacity
    private int capacity;

    public CircularBuffer(int capacity)
    {
        buffer = new List <T>(capacity);
        this.capacity = capacity;
    }

    public int Count => buffer.Count;


    public void Push(T item)
    {
        // check if my buffer is at or above capacity
        if (buffer.Count >= capacity)
        {
            buffer.RemoveAt(0);     // removes the oldest data 
        }
        buffer.Add(item);
    }

    public T Pop()
    {
        if (buffer.Count == 0) return default(T);

        int lastIndex = buffer.Count - 1;
        T item = buffer[lastIndex];
        buffer.RemoveAt(lastIndex);

        return item;
    }
}
