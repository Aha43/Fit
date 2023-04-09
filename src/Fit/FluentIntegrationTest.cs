using Fit.Abstraction;

namespace Fit;

public static class FluentIntegrationTest
{
    public static IFit Create(Action<FitOptions>? o = null) => new FitImplementation(o);
}
