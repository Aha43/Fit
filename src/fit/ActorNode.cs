namespace fit;

public class ActorNode
{
    private readonly Fit _fit;

    private readonly ActorNode? _parent;

    private readonly IActor _actor;

    private readonly TypedMap _parameters = new();

    private readonly List<ActorNode> _nodes = new();

    public ActorNode(Fit fit, IActor actor, ActorNode? parent)
    {
        _fit = fit;
        _actor = actor;
        _parent = parent;
    }

    public bool Root => _parent == null;

    public ActorNode? Parent => _parent;

    public bool Leaf => _nodes.Count == 0;

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

    public ActorNode Do(string name)
    {
        var actor = _fit.GetActor(name);
        if (actor == null) 
        {
            if (_fit.Proto)
            {
                // ToDo
            }
            else 
            { 
            }

            return this;
        }

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

    public async Task ActAsync(TypedMap stateClaims) => 
        await _actor.ActAsync(stateClaims, _parameters).ConfigureAwait(false);

}
