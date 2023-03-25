using Fit.Demo.Specification;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Demo.Infrastructure.InMemory;

public static class Services
{
    public static IServiceCollection AddFitDemoInMemoryInfrastructure(this IServiceCollection services)
    {
        return services.AddSingleton<IToDoApi, InMemoryToDoRepository>()
            .AddSingleton<ITagApi, InMemoryTagRepository>();
    }
}
