namespace HiddenMemoryAllocation;

public class Slide96MemoryWaste
{
    public void IncreaseMemoryUsage()
    {
        using MemoryStream ms = new MemoryStream();
        Console.WriteLine($"Initial Capacity: {ms.Capacity} bytes");

        int lastCapacity = ms.Capacity;
        byte[] buffer = new byte[100024];
    
        for (int i = 0; i < 10000; i++)
        {
            // If there is a shortage of memory, the memory doubles.
            // and copy the previous data to a new buffer
            ms.Write(buffer, 0, buffer.Length);

            if (ms.Capacity > lastCapacity)
            {
                Console.WriteLine($"Capacity increased to: {ms.Capacity} bytes");
                lastCapacity = ms.Capacity;
            }
        }
    }
}