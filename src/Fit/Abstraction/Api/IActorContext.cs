namespace Fit.Abstraction.Api;

public interface IActorContext
{
    string CaseName { get; }
    string? ActorName { get; }
    IStateClaims StateClaims { get; }
    public IActorParameters Parameters { get; }
}
