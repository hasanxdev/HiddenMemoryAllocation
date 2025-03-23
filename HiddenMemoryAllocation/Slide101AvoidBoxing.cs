namespace HiddenMemoryAllocation;

public class Slide101AvoidBoxing
{
    public void Run()
    {
        var myStruct = new MyStruct();
        var toString = myStruct.ToString(); // fix with override
        var hashCode = myStruct.GetHashCode();
        var getType = myStruct.GetType();
        Func<double> action2 = myStruct.SomeMethod;
        
        Console.WriteLine($"MyStruct: {toString}");
        Console.WriteLine($"MyStruct: {hashCode}");
        Console.WriteLine($"MyStruct: {getType}");
    }
}

file struct MyStruct
{
    public override string ToString()
    {
        return nameof(MyStruct);
    }

    public double SomeMethod()
    {
        return 1;
    }
}