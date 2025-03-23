namespace HiddenMemoryAllocation.BenchmarkDotNet.Artifacts;

public class Slide99DelegateAllocation
{
    Func<double> action = () => ProcessWithLogging(); // hidden
    Func<double> action2 = ProcessWithLogging; // hidden

    private static double ProcessWithLogging()
    {
        throw new NotImplementedException();
    }
}