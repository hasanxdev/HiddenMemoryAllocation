## Benchmark Results After Fix

After addressing the issue mentioned in the comments, the benchmark results are as follows:
#### 📹 **Video demonstration:** [https://youtu.be/aFx9xrL0MuA?si=r90DUH2oadNHvxND](https://youtu.be/aFx9xrL0MuA?si=r90DUH2oadNHvxND)

```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3624)
13th Gen Intel Core i7-13700H, 1 CPU, 20 logical and 14 physical cores
.NET SDK 9.0.101
  [Host]     : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
  DefaultJob : .NET 9.0.0 (9.0.24.52809), X64 RyuJIT AVX2
```
| Method     | Mean      | Error     | StdDev    | Allocated |
|----------- |----------:|----------:|----------:|----------:|
| WithShared |  1.693 ms | 0.0274 ms | 0.0256 ms |   3.44 KB |
| WithCreate | 37.058 ms | 1.3538 ms | 3.9492 ms | 107.62 KB |

