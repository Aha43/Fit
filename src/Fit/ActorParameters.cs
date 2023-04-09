using Fit.Abstraction;
using System.Text;

namespace Fit;

internal class ActorParameters : IActorParameters
{
    private readonly Dictionary<string, object> _dict = new();

    private readonly IActorNode _node;

    internal ActorParameters(IActorNode node) => _node = node;

    internal Dictionary<string, object> GetDictionary() => _dict;

    public IActorParameters And(string name, object value)
    {
        _dict[name] = value;
        return this;
    }

    public T Get<T>(string key) => (T)_dict[key];

    public void Remove(string name) => _dict.Remove(name);

    public override string ToString()
    {
        var sb = new StringBuilder();

        var first = true;
        foreach (var item in _dict)
        {
            if (!first) sb.Append(" and ");
            else sb.Append("with ");
            first = false;
            sb.Append($"{item.Key} is {item.Value}");
        }

        return sb.ToString();
    }

    public IActorNode Then(string name) => _node.Then(name);
    public IActorNode Then<T>() where T : IActor => _node.Then<T>();
    public void AsCase(string name) => _node.AsCase(name);
    public void AsSegment(string name) => _node.AsSegment(name);
    public IActorNode ThenContinueWith(string name) => _node.ThenContinueWith(name);
}
