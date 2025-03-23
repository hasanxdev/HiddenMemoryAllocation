using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using HiddenMemoryAllocation;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.ObjectPool;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<MyClass>();
builder.Services.TryAddSingleton<ObjectPool<ReusableBuffer>>(serviceProvider =>
{
    var provider = serviceProvider.GetRequiredService<ObjectPoolProvider>();
    var policy = new DefaultPooledObjectPolicy<ReusableBuffer>();
    return provider.Create(policy);
});

new Slide96RecyclableMemoryStream().MemoryStreamInGen2AndGetSpan();

var app = builder.Build();

app.MapGet("/hash/{name}", (string name, ObjectPool<ReusableBuffer> bufferPool) =>
{
    var buffer = bufferPool.Get();
    try
    {
        // Set the buffer data to the ASCII values of a word
        for (var i = 0; i < name.Length; i++)
        {
            buffer.Data[i] = (byte)name[i];
        }

        Span<byte> hash = stackalloc byte[32];
        SHA256.HashData(buffer.Data.AsSpan(0, name.Length), hash);
        return "Hash: " + Convert.ToHexString(hash);
    }
    finally
    {
        // Data is automatically reset because this type implemented IResettable
        bufferPool.Return(buffer); 
    }
});

app.MapGet("/stackalloc", async (MyClass myClass) =>
{
    for (int i = 0; i < 18; i++)
    {
        Span<long> array = stackalloc long[10018];
    }

    var t1 = Thread.CurrentThread;
    await myClass.RandomCode(10);

    await Task.Delay(100000);
});

app.Run();

public class MyClass
{
    public async Task RandomCode(int count)
    {
        if (RuntimeHelpers.TryEnsureSufficientExecutionStack() is false)
        {
            return;
        }
        
        for (int i = 0; i < count; i++)
        {
            Span<long> array = stackalloc long[10018];
        }
        
        var t1 = Thread.CurrentThread;
        await Task.Yield();
        var t2 = Thread.CurrentThread;
    }
}