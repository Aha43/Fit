using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class FitOptions
{
    public RunMode RunMode { get; } = new();
    public IServiceCollection Services { get; } = new ServiceCollection();
}
