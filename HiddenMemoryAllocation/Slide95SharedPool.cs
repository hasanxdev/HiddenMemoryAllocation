using System.Buffers;

namespace HiddenMemoryAllocation;

public class Slide95SharedPool
{
    public async Task ArrayPoolSharedPoolAsync(params int[] a)
    {
        var arr3 = ArrayPool<byte>.Shared.Rent(2000);
        var arr4 = ArrayPool<byte>.Shared.Rent(2000);
        Array.Fill(arr3, (byte)12);
        Array.Fill(arr4, (byte)12);
        ArrayPool<byte>.Shared.Return(arr3);
        ArrayPool<byte>.Shared.Return(arr4);

        Task.Run(() =>
        {
            var arr5 = ArrayPool<byte>.Shared.Rent(2000);
            Console.WriteLine("hello");
        }).Wait();
    
        await Task.Yield();
    }
}