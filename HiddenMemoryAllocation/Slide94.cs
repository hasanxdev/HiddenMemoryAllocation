using System.Buffers;

namespace HiddenMemoryAllocation;

public class Slide94
{
    public void Func1()
    {
        // arrays longer than 1,048,576 will be allocated and not saved in the pool
        var pool = ArrayPool<int>.Create(1024 * 1024, 50);
        while (true)
        {
            // return exist array with values and set null
            int[] buffer = pool.Rent(1024 * 1024);
            try
            {
                for (int i = 0; i < buffer.Length; i++)
                {
                    buffer[i] = i;
                }
            }
            finally
            {
                pool.Return(buffer);
            }
        }
    }
}