using System.Numerics;
using System.Runtime.CompilerServices;

namespace HiddenMemoryAllocation;

public class Slide100UseGenerics
{
    public void Run()
    {
        object obj = 0; // Int32 struct boxed
        FooBar(0); // 0 will be boxed

        static void FooBar(object obj)
        {
        }
    }

    public void Run2()
    {
        // ValueTuple to ITuple
        FooBar(new ValueTuple() { });

        static void FooBar(ITuple tuple)
        {
            // ValueTuple will be boxed
        }
    }

    public void Run3()
    {
        FooBar(0); // 0 will be boxed

        static void FooBar<T>(T obj) where T : INumber<T>
        {
            var test = T.IsSubnormal(obj);
        }
    }

    public void Run4()
    {
        FooBar(new ValueTuple() { });
        void FooBar<T>(T tuple) where T : ITuple
        {
            // ValueTuple will not be boxed
            Console.WriteLine($"# of elements: {tuple.Length}");
            Console.WriteLine($"Second to last element: {tuple[tuple.Length - 2]}");
        }
    }
}