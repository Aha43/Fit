namespace Fit.Abstraction;

public interface IActor
{
    Task ActAsync(IActorContext context);
}
