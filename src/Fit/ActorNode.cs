using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

public class ActorNode
{
    private readonly Fit _fit;

    private readonly ActorNode? _parent;

    private readonly string _actorName;

    private readonly ActorParameters _parameters = new();

    private ActorNode? _next = null;

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

    public string ActorName => _actorName;

    public ActorNode? Parent => _parent;

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

    public void AsCase(string name) => _fit.AddCase(name, this);

    public void AsStart(string name) => _fit.AddStart(name, this);

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
