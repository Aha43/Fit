using Microsoft.Extensions.DependencyInjection;

namespace Fit
{
    public static class Services
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            var actorTypes = FindNonAbstractTypes<ActorBase>();
            foreach (var t in actorTypes) services.AddSingleton(t);

            var assertorTypes = FindNonAbstractTypes<AssertorBase>();
            foreach (var t in assertorTypes) services.AddSingleton(t);

            return services;
        }

        private static IEnumerable<Type> FindNonAbstractTypes<T>() where T : class
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(T)));
            return types;
        }

    }

}
