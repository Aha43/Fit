using Fit.XUnit;

namespace Fit.Demo.Test;

public class DemoTest : FitXunitTestBase
{
    [Theory]
    [ClassData(typeof(TestSource))]
    public async Task RunCase(Fit fit, string name) => await RunNamedCase(fit, name);
}