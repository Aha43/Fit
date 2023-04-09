using Fit.Abstraction;
using Xunit.Abstractions;

namespace Fit.XUnit;

public abstract class FitXunitTestBase
{
    protected readonly ITestOutputHelper? _output;

    private readonly CaseRunReporter _reporter = new();

    protected FitXunitTestBase(ITestOutputHelper? output = null) => _output = output;

    protected async Task RunNamedCase(IFit fit, string name)
    {
        await fit.RunCase(name, _reporter);
        var report = _reporter.ToString();
        _output?.WriteLine(report);
    }

}
