using Microsoft.Extensions.DependencyInjection;

namespace Fit.Demo.Validation;

public static class Services
{
    public static IServiceCollection AddFitDemoValidation(this IServiceCollection services)
    {
        return services.AddSingleton<ToDoValidator>()
            .AddSingleton<TagValidator>();
    }
}
