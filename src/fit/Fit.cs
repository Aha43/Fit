using fit.Exceptions;

namespace fit;

public class Fit
{
    private readonly Dictionary<string, IActor> _actors = new();

    private readonly Dictionary<string, ActorNode[]> _tests = new();

    private readonly List<IAsserter> _asserters = new();

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

    public Fit AddActor(IActor actor)
    {
        var name = actor.GetType().Name;
        if (_actors.ContainsKey(name) ) 
        {
            throw new DuplicateActorException(name);
        }

        _actors[name] = actor;
        return this;
    }

    public Fit AddAsserter(IAsserter asserter)
    {
        _asserters.Add(asserter);
        return this;
    }

    public Fit RunTest(string name)
    {
        if 
    }

}
