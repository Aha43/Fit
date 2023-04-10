using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

internal class FitImplementation : IFit
{
    private readonly FitSystem _system;

    private readonly Dictionary<string, IActorNode[]> _segments = new();

    private readonly Dictionary<string, IActorNode[]> _cases = new();

    private readonly FitOptions _options = new();
 
    internal FitImplementation(Action<FitOptions>? o = null)
    {
        o?.Invoke(_options);
        _system = new FitSystem(_options.Services);

        var caseDefiners = InstantiateUtil.FindAndInstantiate<ICaseDefiner>();
        foreach (var defines in caseDefiners) defines.AddCases(this);
    }

    public IActorNode First<T>() where T : class => First(typeof(T).Name);

    public IActorNode First(string name) => new ActorNode(this, name);

    public IActorNode FirstDo(string name)
    {
        var start = GetSegment(name);

        ActorNode? retVal = null;
        if (start.Length > 0)
        {
            foreach (var node in start) retVal = new ActorNode(this, node.ActorName, retVal);
            return retVal!;

        }
        throw new InternalFitException($"Start '{name}' the empty array!");
    }

    public IEnumerable<string> CaseNames => _cases.Keys.AsEnumerable();

    public async Task<string> RunCase(string caseName)
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

        var caseRunReporter = new CaseRunReporter();

        caseRunReporter.CaseStart(caseName);

        foreach (var actor in @case)
        {
            await ((ActorNode)actor).ActAsync(context, _options.RunMode, caseRunReporter).ConfigureAwait(false);
            await Assert(context.StateClaims).ConfigureAwait(false);
        }

        tasks.Clear();
        foreach (var tearDown in _system.TearDowns) tasks.Add(tearDown.TearDownAsync(context.StateClaims));
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);

        return caseRunReporter.ToString();
    }

    internal void AddCase(string name, ActorNode end)
    {
        if (_cases.ContainsKey(name)) 
        {
            throw new DuplicateCaseException(name);
        }

        _cases[name] = end.Path();
    }

    internal void AddSegment(string name, ActorNode end)
    {
        if (_segments.ContainsKey(name))
        {
            throw new DuplicateSegmentException(name);
        }

        _segments[name] = end.Path();
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

    internal IActorNode[] GetSegment(string name)
    {
        if (_segments.TryGetValue(name, out var retVal)) return retVal;
        throw new SegmentNotFoundException(name);
    }

    private async Task Assert(IStateClaims stateClaims)
    {
        var tasks = new List<Task>();
        foreach (var assertor in _system.Assertors) 
        {
            tasks.Add(assertor.AssertAsync(stateClaims));
        }
        if (tasks.Count > 0) await Task.WhenAll(tasks).ConfigureAwait(false);
    }

}
