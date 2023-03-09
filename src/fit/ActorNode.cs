namespace fit;

public class ActorNode
{
    private readonly Fit _fit;

    private readonly IActor _actor;

    private readonly TypedMap _parameters = new();

    private readonly List<ActorNode> _nodes = new();

    public ActorNode(Fit fit, IActor actor)
    {
        _fit = fit;
        _actor = actor;
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

        _nodes.Add(new ActorNode(_fit, actor));

        return this;
    }

    public ActorNode With<T>(string name, T value)
    {
        _parameters.Set(name, value);
        return this;
    }

    public async Task ActAsync(TypedMap stateClaims) => 
        await _actor.ActAsync(stateClaims, _parameters).ConfigureAwait(false);

}
