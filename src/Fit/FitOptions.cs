using Fit.Abstraction;
using Microsoft.Extensions.DependencyInjection;

namespace Fit;

internal class FitOptions : IFitOptions
{
    public IRunMode RunMode { get; } = new RunMode();
    public IServiceCollection Services { get; } = new ServiceCollection();
}
