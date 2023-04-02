namespace Fit.Abstraction;

public interface IActor
{
    Task ActAsync(ActorContext context);
}
