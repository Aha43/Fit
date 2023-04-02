namespace Fit;

public sealed class ActorContext
{
    public StateClaims StateClaims { get; } = new();
    public ActorParameters Parameters { get; internal set; } = new();

    internal ActorContext() { }
}
