using Fit.Abstraction.Client;

namespace Fit.Abstraction.Api;

public interface IWriteActorParameters
{
    IWriteActorParameters And(string name, object value);
    IActorNode Then<T>() where T : IActor;
    IActorNode Then(string name);
    void AsCase(string name);
    void AsSegment(string name);
    IActorNode ThenContinueWith(string name);
}
