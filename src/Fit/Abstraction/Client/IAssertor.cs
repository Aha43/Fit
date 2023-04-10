using Fit.Abstraction.Api;

namespace Fit.Abstraction.Client;

public interface IAssertor
{
    Task AssertAsync(IStateClaims stateClaims);
}
