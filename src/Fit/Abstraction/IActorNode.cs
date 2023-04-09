namespace Fit.Abstraction;

public interface IActorNode
{
    public string ActorName { get; }
    public IActorNode Then<T>() where T : IActor;
    public IActorNode Then(string name);
    public IActorNode ThenContinueWith(string name);
    public IActorParameters With<T>(string name, T value);
    public void AsCase(string name);
    public void AsSegment(string name);
}
