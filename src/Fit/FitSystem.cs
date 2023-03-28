using Microsoft.Extensions.DependencyInjection;

namespace Fit;

internal class FitSystem
{
    private readonly IServiceCollection _service;

    private readonly Dictionary<string, IActor> _actors = new();

    internal FitSystem(IServiceCollection service) => _service = service;

    private IServiceProvider? _serviceProvider;

    private readonly List<IAssertor> _assertors = new();

    internal IEnumerable<IAssertor> Assertors => _assertors.AsReadOnly();

    internal void BuildSystem()
    {
        _serviceProvider = AddServices(_service).BuildServiceProvider();

        _actors.Clear();
        foreach (var at in _actorTypes)
        {
            if (_serviceProvider.GetService(at.Value) is IActor actor) _actors.Add(at.Key, actor);
        }

        _assertors.Clear();
        foreach (var t in _assertorTypes)
        {
          if (_serviceProvider.GetService(t) is IAssertor assertor) _assertors.Add(assertor);
        }
    }

    internal IActor? GetActorByName(string name)
    {
        if (_serviceProvider == null)
        {
            throw new Exception();
        }

        if (_actors.TryGetValue(name, out IActor? actorCached)) return actorCached;
        if (_actorTypes.TryGetValue(name, out Type? type))
        {
            if (_serviceProvider.GetService(type) is IActor actor)
            {
                _actors.Add(name, actor);
                return actor;
            }
        }

        return null;
    }

    private readonly Dictionary<string, Type> _actorTypes = new();
    private readonly List<Type> _assertorTypes = new();

    private IServiceCollection AddServices(IServiceCollection services)
    {
        _actorTypes.Clear();
        var actorTypes = FindNonAbstractTypes<IActor>();
        foreach (var t in actorTypes)
        {
            services.AddSingleton(t);
            _actorTypes.Add(t.Name, t);
        }

        _assertorTypes.Clear();
        var assertorTypes = FindNonAbstractTypes<IAssertor>();
        foreach (var t in assertorTypes)
        {
            services.AddSingleton(t);
            _assertorTypes.Add(t);
        }

        return services;
    }

    private static IEnumerable<Type> FindNonAbstractTypes<T>() where T : class
    {
        var tType = typeof(T);
        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => !type.IsAbstract &&
                !type.IsInterface &&
                tType.IsAssignableFrom(type));
        return types;
    }

}
