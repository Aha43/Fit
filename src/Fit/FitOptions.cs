using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class FitOptions
{
    public IServiceCollection Services { get; } = new ServiceCollection();
}
