using Fit.Abstraction.Api;

namespace Fit.Abstraction.Client;

public interface IActor
{
    Task ActAsync(IActorContext context);
}
