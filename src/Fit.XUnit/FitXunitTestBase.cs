using Fit.Abstraction;
using Xunit.Abstractions;

namespace Fit.XUnit;

public abstract class FitXunitTestBase
{
    protected readonly ITestOutputHelper? _output;

    protected FitXunitTestBase(ITestOutputHelper? output = null) => _output = output;

    protected async Task RunNamedCase(IFit fit, string name)
    {
        var report = await fit.RunCase(name);
        _output?.WriteLine(report);
    }

}
