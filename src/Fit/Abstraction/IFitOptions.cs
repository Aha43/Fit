using Microsoft.Extensions.DependencyInjection;

namespace Fit.Abstraction;

public interface IFitOptions
{
    IRunMode RunMode { get; }
    IServiceCollection Services { get; }
}
