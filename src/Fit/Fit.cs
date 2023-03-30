using Fit.Exceptions;

namespace Fit;

public class Fit : IDo
{
    private readonly FitSystem _system;

    private readonly List<ActorNode> _roots = new();
    private readonly Dictionary<string, ActorNode[]> _tests = new();

    private readonly FitOptions _options = new();
 
    public Fit(Action<FitOptions>? o = null)
    {
        o?.Invoke(_options);
        _system = new FitSystem(_options.Services);
    }

    internal void AddTest(string name, ActorNode end)
    {
        if (_tests.ContainsKey(name)) 
        {
            throw new DuplicateTestException(name);
        }

        _tests[name] = end.Path();
    }

    internal IActor GetActor(string name) 
    {
        var retVal = _system.GetActorByName(name);
        retVal ??= _system.GetActorByName($"{name}Actor");
        if (retVal == null)
        {
            throw new ActorNotFoundException($"{name} or {name}Actor");
        }

        return retVal;
    }

    public ActorNode Do<T>() where T : class => Do(typeof(T).Name);

    public ActorNode Do(string name)
    {
        var root = new ActorNode(this, name);
        _roots.Add(root);
        return root;
    }

    public async Task RunTest(string name)
    {
        if (!_tests.ContainsKey(name)) 
        {
            throw new TestNotFoundException(name);
        }

        var stateClaims = new StateClaims();

        var test = _tests[name];

        _system.BuildSystem();

        var tasks = new List<Task>();
        foreach (var setUp in _system.SetUps) tasks.Add(setUp.SetUpAsync(stateClaims));
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);

        foreach (var actor in test) 
        {
            await actor.ActAsync(stateClaims).ConfigureAwait(false);
            await Assert(stateClaims).ConfigureAwait(false);
        }

        tasks.Clear();
        foreach (var tearDown in _system.TearDowns) tasks.Add(tearDown.TearDownAsync(stateClaims));
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);
    }

    private async Task Assert(StateClaims stateClaims)
    {
        var tasks = new List<Task>();
        foreach (var assertor in _system.Assertors) 
        {
            tasks.Add(assertor.AssertAsync(stateClaims));
        }
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);
    }

}
