namespace Fit;

public interface IActor
{
    Task ActAsync(StateClaims stateClaims, ActorParameters parameters);
}
