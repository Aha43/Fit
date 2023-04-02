using Fit.Exceptions;

namespace Fit;

public class Fit : IDo
{
    private readonly FitSystem _system;

    private readonly List<ActorNode> _roots = new();
    private readonly Dictionary<string, ActorNode[]> _cases = new();

    private readonly FitOptions _options = new();
 
    public Fit(Action<FitOptions>? o = null)
    {
        o?.Invoke(_options);
        _system = new FitSystem(_options.Services);

        var caseDefiners = InstantiateUtil.FindAndInstantiate<ICaseDefiner>();
        foreach (var defines in caseDefiners) defines.AddCases(this);
    }

    internal void AddCase(string name, ActorNode end)
    {
        if (_cases.ContainsKey(name)) 
        {
            throw new DuplicateTestException(name);
        }

        _cases[name] = end.Path();
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

    public IEnumerable<string> CaseNames => _cases.Keys.AsEnumerable();

    public async Task RunCase(string name)
    {
        if (!_cases.ContainsKey(name)) 
        {
            throw new TestNotFoundException(name);
        }

        var context = new ActorContext();

        var @case = _cases[name];

        _system.BuildSystem();

        var tasks = new List<Task>();
        foreach (var setUp in _system.SetUps) tasks.Add(setUp.SetUpAsync(context.StateClaims));
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);

        foreach (var actor in @case) 
        {
            await actor.ActAsync(context).ConfigureAwait(false);
            await Assert(context.StateClaims).ConfigureAwait(false);
        }

        tasks.Clear();
        foreach (var tearDown in _system.TearDowns) tasks.Add(tearDown.TearDownAsync(context.StateClaims));
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
