using Microsoft.Extensions.DependencyInjection;

namespace Fit.Abstraction.Api;

public interface IFitOptions
{
    IRunMode RunMode { get; }
    IServiceCollection Services { get; }
}
