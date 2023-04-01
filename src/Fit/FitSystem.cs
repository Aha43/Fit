using Microsoft.Extensions.DependencyInjection;

namespace Fit;

internal class FitSystem
{
    private readonly IServiceCollection _service;

    private readonly ActorManager _actorManager = new();

    private readonly InstanceManager<IAssertor> _assertorManager = new();
    internal IEnumerable<IAssertor> Assertors => _assertorManager.Instances;

    private readonly InstanceManager<ISetUp> _setUpManager = new();
    internal IEnumerable<ISetUp> SetUps => _setUpManager.Instances;

    private readonly InstanceManager<ITearDown> _tearDownManager = new();
    internal IEnumerable<ITearDown> TearDowns => _tearDownManager.Instances;

    internal FitSystem(IServiceCollection service) => _service = service;

    internal void BuildSystem()
    {
        var serviceProvider = AddServices(_service).BuildServiceProvider();

        _actorManager.Instantiate(serviceProvider);
        _assertorManager.Instantiate(serviceProvider);
        _setUpManager.Instantiate(serviceProvider);
        _tearDownManager.Instantiate(serviceProvider);
    }

    internal IActor? GetActorByName(string name) => _actorManager.GetActor(name);
    
    private IServiceCollection AddServices(IServiceCollection services)
    {
        _actorManager.AddServices(services);
        _assertorManager.AddServices(services);
        _setUpManager.AddServices(services);
        _tearDownManager.AddServices(services);

        return services;
    }

}

internal class InstanceManager<T> where T : class
{
    private readonly List<Type> _types = new();
    private readonly List<T> _instances = new();
    internal IEnumerable<T> Instances => _instances.AsReadOnly();

    internal void AddServices(IServiceCollection services)
    {
        _types.Clear();
        var types = Util.FindNonAbstractTypes<T>();
        foreach (var t in types)
        {
            _types.Add(t);
            services.AddSingleton(t);
        }
    }

    internal void Instantiate(IServiceProvider serviceProvider)
    {
        _instances.Clear();
        foreach (var t in _types)
        {
            if (serviceProvider.GetService(t) is T instance) _instances.Add(instance);
        }
    }

}

internal class ActorManager
{
    private readonly Dictionary<string, Type> _actorTypes = new();
    private readonly Dictionary<string, IActor> _actors = new();

    internal IActor? GetActor(string name)
    {
        if (_actors.TryGetValue(name, out IActor? actor)) return actor;
        return null;
    }

    internal void AddServices(IServiceCollection services)
    {
        _actorTypes.Clear();
        var actorTypes = Util.FindNonAbstractTypes<IActor>();
        foreach (var t in actorTypes)
        {
            services.AddSingleton(t);
            _actorTypes.Add(t.Name, t);
        }
    }

    internal void Instantiate(IServiceProvider serviceProvider)
    {
        _actors.Clear();
        foreach (var at in _actorTypes)
        {
            if (serviceProvider.GetService(at.Value) is IActor actor) _actors.Add(at.Key, actor);
        }
    }

}
