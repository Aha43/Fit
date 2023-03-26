﻿namespace Fit;

public class ActorNode
{
    private readonly Fit _fit;

    private readonly ActorNode? _parent;

    private readonly IActor? _actor;

    private readonly TypedMap _parameters = new();

    private readonly List<ActorNode> _nodes = new();

    internal ActorNode(Fit fit, IActor? actor, ActorNode? parent = null)
    {
        _fit = fit;
        _actor = actor;
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

    public bool Leaf => _nodes.Count == 0;

    public ActorNode Do<T>() where T : class => Do(typeof(T).Name);

    public ActorNode Do(string name)
    {
        var actor = _fit.GetActor(name);
        _nodes.Add(new ActorNode(_fit, actor, this));
        return this;
    }

    public ActorNode With<T>(string name, T value)
    {
        _parameters.Set(name, value);
        return this;
    }

    public ActorNode AsCase(string name) 
    {
        _fit.AddTest(name, this);
        return this;
    }

    public async Task ActAsync(TypedMap stateClaims)
    {
        if (_actor == null)
        {
            // ToDo
        }
        else
        {
            await _actor.ActAsync(stateClaims, _parameters).ConfigureAwait(false);
        }
    }
        
}
