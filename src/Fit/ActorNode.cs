using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

internal class ActorNode : IActorNode
{
    private readonly Fit _fit;

    private readonly IActorNode? _parent;

    private readonly string _actorName;

    private readonly ActorParameters _parameters = new();

    private IActorNode? _next = null;

    internal ActorNode(Fit fit, string actorName, ActorNode? parent = null, ActorParameters? @params = null)
    {
        _fit = fit;
        _actorName = actorName;
        _parent = parent;
        if (@params != null) 
        {
            foreach (var param in @params) _parameters[param.Key] = param.Value;
        }
    }

    internal IActorNode[] Path()
    {
        var stack = new Stack<IActorNode>();
        ActorNode? curr = this;
        while (curr != null)
        {
            stack.Push(curr);
            curr = curr._parent as ActorNode;
        }

        return stack.ToArray();
    }

    public string ActorName => _actorName;

    public IActorNode Do<T>() where T : class => Do(typeof(T).Name);

    public IActorNode Do(string name)
    {
        if (_next != null)
        {
            throw new NextActAllreadyDefinedException();
        }
        _next = new ActorNode(_fit, name, this);
        return _next;
    }

    public IActorNode ContinueWith(string name)
    {
        if (_next != null) 
        {
            throw new NextActAllreadyDefinedException();
        }

        var segment = _fit.GetSegment(name);
        var parent = this;
        foreach (var node in segment)
        {
            parent = new ActorNode(_fit, node.ActorName, parent);
        }

        return parent;
    }

    public IActorNode With<T>(string name, T value)
    {
        if (value == null)
        {
            _parameters.Remove(name);
            return this;
        }

        _parameters.Add(name, value);
        return this;
    }

    public void AsCase(string name) => _fit.AddCase(name, this);

    public void AsSegment(string name) => _fit.AddSegment(name, this);

    internal async Task ActAsync(ActorContext context, RunMode run, CaseRunReporter? caseRunReporter)
    {
        var actor = _fit.GetActor(_actorName);

        context.ActorName = _actorName;
        context.Parameters = _parameters;
        if (actor != null && !run.Proto)
        {
            await actor.ActAsync(context).ConfigureAwait(false);
        }

        caseRunReporter?.ActorPerforms(context, run.Proto, actor != null);
    }
        
}
