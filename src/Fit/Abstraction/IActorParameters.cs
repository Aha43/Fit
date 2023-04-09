namespace Fit.Abstraction;

public interface IActorParameters
{
    T Get<T>(string key);
    IActorParameters And(string name, object value);
    void Remove(string name);
    IActorNode Then<T>() where T : IActor;
    IActorNode Then(string name);
    void AsCase(string name);
    void AsSegment(string name);
    IActorNode ThenContinueWith(string name);
}
