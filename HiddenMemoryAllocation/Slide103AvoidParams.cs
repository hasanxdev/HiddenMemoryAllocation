using System.Collections;

namespace HiddenMemoryAllocation;

public class Slide103AvoidParams
{
    public void Run()
    {
        MyMethod(1, 2, 3);
        MyMethod(1);
    }

    public void MyMethod(int index, params int[] args)
    {
        Span<int> myArray = stackalloc int[args.Length];
        Console.WriteLine(myArray[index]);
        
        
        var list = new List<int>(1000);
        var stack = new Stack<int>(1000);
        var queue = new  Queue<int>(1000);
        var sortedList = new SortedList(1000);
    }
}

public ref struct MyTest
{
    public string Test { get; set; }
    public int[] Test2 { get; set; }
}