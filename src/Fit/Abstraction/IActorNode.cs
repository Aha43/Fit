namespace Fit.Abstraction;

public interface IActorNode
{
    public string ActorName { get; }
    public IActorNode Then<T>() where T : class;
    public IActorNode Then(string name);
    public IActorNode ThenContinueWith(string name);
    public IActorNode With<T>(string name, T value);
    public void AsCase(string name);
    public void AsSegment(string name);
}
