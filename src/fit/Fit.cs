namespace fit;

public class Fit
{
    private readonly Dictionary<string, IActor> _actors = new();

    public bool Proto { get; set; }

    internal void AddTest(string name, ActorNode end)
    {

    }

    public IActor? GetActor(string name)
    {
        if (_actors.TryGetValue(name, out IActor? actor)) return actor;
        return null;
    }

}
