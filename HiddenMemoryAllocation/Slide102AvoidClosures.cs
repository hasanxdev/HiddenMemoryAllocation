namespace HiddenMemoryAllocation;

public class Slide102AvoidClosures
{
    public List<int> _list { get; set; }

    private IEnumerable<string> WithClosures(int myValue)
    {
        // Closures in low-level C# are implemented as classes, and classes incur allocation.
        // The more parameters are captured by the closure, the larger the size of this class becomes
        // Because using a lambda expression, the code has hidden allocation
        var filteredList = _list.Where(x => x > myValue);
        var result = filteredList.Select(x => x.ToString());
        return result;
    }

    private IEnumerable<string> WithClosuresWithLocalFunction(int value)
    {
        bool WhereCondition(int x) => x > value;
        string SelectAction(int x) => x.ToString();
        var filteredList = _list.Where(WhereCondition);
        var result = filteredList.Select(SelectAction);
        return result;
    }
    
    private IEnumerable<string> WithStaticDelegate(int myValue)
    {
        var filteredList = _list.Where(static x => x > 11);
        var result = filteredList.Select(static x => x.ToString());
        return result;
    }

    private IEnumerable<string> WithoutClosures(int value)
    {
        List<string> result = new List<string>();
        foreach (int x in _list)
        {
            if (x > value)
            {
                result.Add(x.ToString());
            }
        }

        return result;
    }

    // IAsyncEnumerator
    private IEnumerable<string> WithYieldReturn(int value)
    {
        // Hidden Allocation
        foreach (int x in _list)
            if (x > value)
                yield return x.ToString();
    }
}