using System.Collections.Generic;

public class CircularBuffer
{
    // collection itself
    private List<int> buffer;
    // Capacity
    private int capacity;

    public CircularBuffer(int capacity)
    {
        buffer = new List <int>(capacity);
        this.capacity = capacity;
    }

    public int Count => buffer.Count;


    public void Push(int item)
    {
        // check if my buffer is at or above capacity
        if (buffer.Count >= capacity)
        {
            buffer.RemoveAt(0);     // removes the oldest data 
        }
        buffer.Add(item);
    }

    public int Pop()
    {
        if (buffer.Count == 0) return -1;

        int lastIndex = buffer.Count - 1;
        int item = buffer[lastIndex];
        buffer.RemoveAt(lastIndex);

        return item;
    }
}
