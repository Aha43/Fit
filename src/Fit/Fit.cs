using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

public class Fit
{
    private readonly FitSystem _system;

    private readonly Dictionary<string, ActorNode[]> _starts = new();

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
            throw new DuplicateCaseException(name);
        }

        _cases[name] = end.Path();
    }

    internal void AddStart(string name, ActorNode end)
    {
        if (_starts.ContainsKey(name))
        {
            throw new DuplicateStartException(name);
        }

        _starts[name] = end.Path();
    }

    internal IActor? GetActor(string name) 
    {
        var retVal = _system.GetActorByName(name);
        var name2 = $"{name}Actor";
        retVal ??= _system.GetActorByName(name2);
        if (retVal == null && !_options.RunMode.IgnoreMissingActors)
        {
            throw new ActorNotFoundException(name, name2);
        }
        return retVal;
    }

    public ActorNode Do<T>() where T : class => Do(typeof(T).Name);

    public ActorNode Do(string name) => new(this, name);

    public ActorNode FromStart(string name)
    {
        if (_starts.TryGetValue(name, out var start))
        {
            ActorNode? retVal = null;
            if (start.Length > 0)
            {
                foreach (var node in start) retVal = new ActorNode(this, node.ActorName, retVal);
                return retVal!;
                
            }
            throw new InternalFitException($"Start '{name}' the empty array!");
        }

        throw new StartNotFoundException(name);
    }

    public IEnumerable<string> CaseNames => _cases.Keys.AsEnumerable();

    public async Task RunCase(string caseName, CaseRunReporter? caseRunReporter = null)
    {
        if (!_cases.ContainsKey(caseName)) 
        {
            throw new CaseNotFoundException(caseName);
        }

        var context = new ActorContext(caseName);

        var @case = _cases[caseName];

        _system.BuildSystem();

        var tasks = new List<Task>();
        foreach (var setUp in _system.SetUps) tasks.Add(setUp.SetUpAsync(context.StateClaims));
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);

        caseRunReporter?.CaseStart(caseName);

        foreach (var actor in @case) 
        {
            await actor.ActAsync(context, _options.RunMode, caseRunReporter).ConfigureAwait(false);
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
