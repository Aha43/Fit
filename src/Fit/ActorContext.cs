namespace Fit;

public sealed class ActorContext
{
    public string CaseName { get; }

    public string? ActorName { get; internal set; }

    public StateClaims StateClaims { get; } = new();
    public ActorParameters Parameters { get; internal set; } = new();

    internal ActorContext(string caseName) => CaseName = caseName; 
}
