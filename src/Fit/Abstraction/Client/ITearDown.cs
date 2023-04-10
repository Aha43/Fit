using Fit.Abstraction.Api;

namespace Fit.Abstraction.Client;

public interface ITearDown
{
    Task TearDownAsync(IStateClaims stateClaims);
}
