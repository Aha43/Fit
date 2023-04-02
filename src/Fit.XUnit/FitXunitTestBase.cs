namespace Fit.XUnit;

public abstract class FitXunitTestBase
{
    protected async Task RunNamedCase(Fit fit, string name) => await fit.RunCase(name);
}
