using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit.Implementation;

internal class ActorNode : IActorNode
{
    private readonly FitImplementation _fit;

    private readonly IActorNode? _parent;

    private readonly string _actorName;

    private readonly WriteActorParameters _parameters;

    private IActorNode? _next = null;

    internal ActorNode(FitImplementation fit, string actorName, ActorNode? parent = null, WriteActorParameters? @params = null)
    {
        _fit = fit;
        _actorName = actorName;
        _parent = parent;
        _parameters = new WriteActorParameters(this);
        if (@params != null)
        {
            foreach (var param in @params.GetDictionary()) _parameters.GetDictionary()[param.Key] = param.Value;
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

    #region InterfaceImpl
    public string ActorName => _actorName;

    public IActorNode Then<T>() where T : IActor => Then(typeof(T).Name);

    public IActorNode Then(string name)
    {
        if (_next != null)
        {
            throw new NextActAllreadyDefinedException();
        }
        _next = new ActorNode(_fit, name, this);
        return _next;
    }

    public IActorNode ThenContinueWith(string name)
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

    public IWriteActorParameters With<T>(string name, T value)
    {
        if (value == null) throw new ArgumentNullException(nameof(value));

        _parameters.And(name, value);
        return _parameters;
    }

    public void AsCase(string name) => _fit.AddCase(name, this);

    public void AsSegment(string name) => _fit.AddSegment(name, this);

    internal async Task ActAsync(ActorContext context, IRunMode run, CaseRunReporter caseRunReporter)
    {
        var actor = _fit.GetActor(_actorName);

        context.ActorName = _actorName;
        context.Parameters = new ActorParameters(_parameters.GetDictionary());
        if (actor != null && !run.Proto)
        {
            await actor.ActAsync(context).ConfigureAwait(false);
        }

        caseRunReporter.ActorPerforms(context, run.Proto, actor != null);
    }
    #endregion

}
