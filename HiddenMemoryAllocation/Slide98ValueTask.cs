using Microsoft.Extensions.ObjectPool;

namespace HiddenMemoryAllocation;

public class Slide98ValueTask
{
    public async Task<string> ReadFileAsync(string filename)
    {
        if (!File.Exists(filename))
            return string.Empty;
        return await File.ReadAllTextAsync(filename);
    }
    
    public async ValueTask<string> ReadFileAsync2(string filename)
    {
        if (!File.Exists(filename))
            return string.Empty;
        return await File.ReadAllTextAsync(filename);
    }

    public async Task<string> UseReadFile(string filename)
    {
        var valueTask = ReadFileAsync2(filename);
        if (valueTask.IsCompleted)
        {
            return valueTask.Result;
        }
        else
        {
            return await valueTask.AsTask();
        }
    }
}