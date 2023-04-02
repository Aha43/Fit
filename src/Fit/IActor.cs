namespace Fit;

public interface IActor
{
    Task ActAsync(ActorContext context);
}
