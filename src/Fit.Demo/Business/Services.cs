using Fit.Demo.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace Fit.Demo.Business;
public static class Services
{
    public static IServiceCollection AddFitDemoBusiness(this IServiceCollection services)
    {
        return services.AddFitDemoValidation()
            .AddSingleton<ToDoViewController>();
    }
}
