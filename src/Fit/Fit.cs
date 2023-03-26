using Fit.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class Fit
{
    private readonly Dictionary<string, IActor> _actors = new();

    private readonly List<ActorNode> _roots = new();
    private readonly Dictionary<string, ActorNode[]> _tests = new();

    private readonly List<AssertorBase> _assertors = new();

    private readonly FitOptions _options = new();

    private readonly IServiceProvider _serviceProvider;

    private readonly Dictionary<string, Type> _actorTypes = new();
    private readonly List<Type> _assertorTypes = new();

    public Fit(Action<FitOptions>? o = null)
    {
        o?.Invoke(_options);

        AddServices(_options.Services);
        _serviceProvider = _options.Services.BuildServiceProvider();
    }

    private void AddServices(IServiceCollection services)
    {
        var actorTypes = Util.FindNonAbstractTypes<IActor>();
        foreach (var t in actorTypes)
        {
            services.AddSingleton(t);
            _actorTypes.Add(t.Name, t);
        }

        var assertorTypes = Util.FindNonAbstractTypes<AssertorBase>();
        foreach (var t in assertorTypes)
        {
            services.AddSingleton(t);
            _assertorTypes.Add(t);
        }
    }

    public bool Proto => _options.Proto;

    internal void AddTest(string name, ActorNode end)
    {
        if (_tests.ContainsKey(name)) 
        {
            throw new DuplicateTestException(name);
        }

        _tests[name] = end.Path();
    }

    internal IActor? GetActor(string name) 
    {
        var retVal = GetActorByName(name);
        retVal ??= GetActorByName($"{name}Actor");
        return retVal;
    }

    private IActor? GetActorByName(string name) 
    {
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

    public ActorNode Do(string name)
    {
        var actor = GetActor(name);
        var root = new ActorNode(this, actor);
        _roots.Add(root);
        return root;
    }

    public async Task RunTest(string name)
    {
        if (!_tests.ContainsKey(name)) 
        {
            throw new TestNotFoundException(name);
        }

        var systemClaim = new TypedMap();

        var test = _tests[name];
        foreach (var t in test) 
        {
            await t.ActAsync(systemClaim).ConfigureAwait(false);
            await Assert(systemClaim).ConfigureAwait(false);
        }
    }

    private async Task Assert(TypedMap systemClaims)
    {
        EnsureAssertorsInstated();
        foreach (var assertor in _assertors) 
        {
            await assertor.AssertAsync(systemClaims).ConfigureAwait(false);
        }
    }

    private void EnsureAssertorsInstated()
    {
        if (_assertors.Any()) return;
        if (!_assertorTypes.Any()) return;

        foreach (var t in _assertorTypes)
        {
            if (_serviceProvider.GetService(t) is AssertorBase assertor) _assertors.Add(assertor);
        }
    }

}
