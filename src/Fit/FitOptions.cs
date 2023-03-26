using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class FitOptions
{
    public bool Proto { get; set; } = false;
    public IServiceCollection Services { get; } = new ServiceCollection();
}
