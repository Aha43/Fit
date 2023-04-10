using Fit.Abstraction.Api;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Implementation;

internal class FitOptions : IFitOptions
{
    public IRunMode RunMode { get; } = new RunMode();
    public IServiceCollection Services { get; } = new ServiceCollection();
}
