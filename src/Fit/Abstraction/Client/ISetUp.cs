using Fit.Abstraction.Api;

namespace Fit.Abstraction.Client;

public interface ISetUp
{
    Task SetUpAsync(IStateClaims stateClaims);
}
