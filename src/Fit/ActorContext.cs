using Fit.Abstraction;

namespace Fit;

public sealed class ActorContext
{
    public string CaseName { get; }

    public string? ActorName { get; internal set; }

    public StateClaims StateClaims { get; } = new();

    public IActorParameters? Parameters { get; internal set; }

    internal ActorContext(string caseName) => CaseName = caseName; 
}
