using Microsoft.IO;

namespace HiddenMemoryAllocation;

public class Slide96RecyclableMemoryStream
{
    private static readonly RecyclableMemoryStreamManager Manager =
        new(new RecyclableMemoryStreamManager.Options()
        {
            BlockSize = 84 * 1000,
            MaximumLargePoolFreeBytes = 1000 * 1000 * 30,
            MaximumSmallPoolFreeBytes = 1000 * 1000 * 30,
        });
    
    public void MemoryStreamInGen2()
    {
        List<MemoryStream> msList = new();
        for (int i = 0; i < 5000; i++)
        {
            msList.Add(Manager.GetStream(new byte[100 * 1000]));
        }
        Console.WriteLine(msList.Count);
    }
    
    public void MemoryStreamInLoh()
    {
        List<MemoryStream> msList = new();
        for (int i = 0; i < 5000; i++)
        {
            msList.Add(new MemoryStream(new byte[85 * 1000]));
        }
        Console.WriteLine(msList.Count);
    }
    
    public void MemoryStreamInGen2AndGetBuffer()
    {
        using var ms = Manager.GetStream();
        for (int i = 0; i < 100; i++)
        {
            ms.Write(new byte[84 * 1000]);
        }

        var allBytes = ms.GetBuffer();
        allBytes = ms.GetBuffer();
        Console.WriteLine(allBytes.Length);
    }
    
    public void MemoryStreamInGen2AndGetSpan()
    {
        using var ms = Manager.GetStream();
        for (int i = 0; i < 100; i++)
        {
            ms.Write(new byte[84 * 1000]);
        }

        var allBytes = ms.GetSpan();
        allBytes = ms.GetSpan();
        Console.WriteLine(allBytes.Length);
    }
}