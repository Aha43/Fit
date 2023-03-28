using Fit.Exceptions;
using System.Diagnostics.Metrics;
using System.Numerics;

namespace Fit;

public class ActorNode : IDo
{
    private readonly Fit _fit;

    private readonly ActorNode? _parent;

    private readonly string _actorName;

    private readonly ActorParameters _parameters = new();

    private ActorNode? _next = null;

    internal ActorNode(Fit fit, string actorName, ActorNode? parent = null)
    {
        _fit = fit;
        _actorName = actorName;
        _parent = parent;
    }

    internal ActorNode[] Path()
    {
        var stack = new Stack<ActorNode>();
        var curr = this;
        while (curr != null)
        {
            stack.Push(curr);
            curr = curr.Parent;
        }

        return stack.ToArray();
    }

    public bool Root => _parent == null;

    public ActorNode? Parent => _parent;

    public bool Last => _next == null;

    public ActorNode Do<T>() where T : class => Do(typeof(T).Name);

    public ActorNode Do(string name)
    {
        if (_next != null)
        {
            throw new NextActAllreadyDefinedException();
        }

        _next = new ActorNode(_fit, name, this);
        return _next;
    }

    public ActorNode With<T>(string name, T value)
    {
        if (value == null)
        {
            _parameters.Remove(name);
            return this;
        }

        _parameters.Add(name, value);
        return this;
    }

    public ActorNode AsCase(string name) 
    {
        _fit.AddTest(name, this);
        return this;
    }

    public async Task ActAsync(StateClaims stateClaims)
    {
        var actor = _fit.GetActor(_actorName);
        if (actor != null)
        {
            await actor.ActAsync(stateClaims, _parameters).ConfigureAwait(false);
        }
    }
        
}
