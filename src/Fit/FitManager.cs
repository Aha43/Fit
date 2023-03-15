using Fit.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Fit;

public class FitManager
{
    private readonly Dictionary<string, ActorBase> _actors = new();

    private readonly Dictionary<string, ActorNode[]> _tests = new();

    private readonly List<AssertorBase> _assertors = new();

    private readonly FitManagerOptions _options = new();

    private readonly IServiceProvider _serviceProvider;

    public FitManager(Action<FitManagerOptions>? o = null)
    {
        o?.Invoke(_options);

        _serviceProvider = _options.Services.AddServices().BuildServiceProvider();
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

    internal ActorBase? GetActor(string name)
    {
        if (_actors.TryGetValue(name, out ActorBase? actor)) return actor;
        return null;
    }

    public FitManager AddActor(ActorBase actor)
    {
        var name = actor.GetType().Name;
        if (_actors.ContainsKey(name) ) 
        {
            throw new DuplicateActorException(name);
        }

        _actors[name] = actor;
        return this;
    }

    public FitManager AddAssertor(AssertorBase assertor)
    {
        _assertors.Add(assertor);
        return this;
    }

    public async Task RunTest(string name)
    {
        if (_tests.ContainsKey(name)) 
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
        foreach (var assertor in _assertors) 
        {
            await assertor.AssertAsync(systemClaims).ConfigureAwait(false);
        }
    }

    


}
