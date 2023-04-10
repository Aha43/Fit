using Fit.Abstraction;

namespace Fit.Implementation;

internal class WriteActorParameters : IWriteActorParameters
{
    private readonly Dictionary<string, object> _parameters = new();

    private readonly IActorNode _node;

    internal WriteActorParameters(IActorNode node) => _node = node;

    internal IDictionary<string, object> GetDictionary() => _parameters;

    public IWriteActorParameters And(string name, object value)
    {
        _parameters[name] = value;
        return this;
    }

    #region InterfaceImpl
    public IActorNode Then(string name) => _node.Then(name);
    public IActorNode Then<T>() where T : IActor => _node.Then<T>();
    public void AsCase(string name) => _node.AsCase(name);
    public void AsSegment(string name) => _node.AsSegment(name);
    public IActorNode ThenContinueWith(string name) => _node.ThenContinueWith(name);
    #endregion

}
