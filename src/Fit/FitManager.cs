using Fit.Exceptions;

namespace Fit;

public class FitManager
{
    private readonly Dictionary<string, IActor> _actors = new();

    private readonly Dictionary<string, ActorNode[]> _tests = new();

    private readonly List<IAssertor> _assertors = new();

    public bool Proto { get; set; }

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
        if (_actors.TryGetValue(name, out IActor? actor)) return actor;
        return null;
    }

    public FitManager AddActor(IActor actor)
    {
        var name = actor.GetType().Name;
        if (_actors.ContainsKey(name) ) 
        {
            throw new DuplicateActorException(name);
        }

        _actors[name] = actor;
        return this;
    }

    public FitManager AddAssertor(IAssertor assertor)
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
