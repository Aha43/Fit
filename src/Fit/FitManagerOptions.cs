using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class FitManagerOptions
{
    public bool Proto { get; set; } = false;
    public IServiceCollection Services { get; } = new ServiceCollection();
}
