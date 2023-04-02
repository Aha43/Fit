namespace Fit.XUnit;

public abstract class XunitTestBase
{
    protected async Task RunNamedCase(Fit fit, string name) => await fit.RunCase(name);
}
