namespace Fit.Abstraction;

public interface IActorContext
{
    string CaseName { get; }
    string? ActorName { get; }
    StateClaims StateClaims { get; }
    public IActorParameters Parameters { get; }
}
