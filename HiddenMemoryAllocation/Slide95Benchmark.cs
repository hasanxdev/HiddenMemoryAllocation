using System.Buffers;
using BenchmarkDotNet.Attributes;

namespace HiddenMemoryAllocation;

[MemoryDiagnoser(true)]
public class Slide95Benchmark
{
    [Benchmark]
    public void WithShared()
    {
        var arr1 = ArrayPool<long>.Shared.Rent(2000);
        Array.Fill(arr1, 999);
        ArrayPool<long>.Shared.Return(arr1);

        Parallel.For(0, 100000, new ParallelOptions()
        {
            MaxDegreeOfParallelism = 10
        }, _ =>
        {
            var arr2 = ArrayPool<long>.Shared.Rent(1000);
            Array.Fill(arr2, 888);
            ArrayPool<long>.Shared.Return(arr2);
        });
    }

    [Benchmark]
    public void WithCreate()
    {
        var pool = ArrayPool<long>.Create();
        var arr1 = pool.Rent(2000);
        Array.Fill(arr1, 999);
        pool.Return(arr1);

        Parallel.For(0, 100000,new ParallelOptions()
        {
            MaxDegreeOfParallelism = 10
        }, _ =>
        {
            var arr2 = pool.Rent(1000);
            Array.Fill(arr2, 888);
            ArrayPool<long>.Shared.Return(arr2);
        });
    }
}