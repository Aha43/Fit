using Fit.Abstraction.Api;
using Fit.XUnit;
using Xunit.Abstractions;

namespace Fit.Demo.Test;

public class DemoTest_Method2 : FitXunitTestBase
{
    public DemoTest_Method2(ITestOutputHelper testReporter) : base(testReporter) { }

    [Theory]
    [ClassData(typeof(TestSource))]
    public async Task RunCases(IFit fit, string name) => await RunNamedCase(fit, name);
}