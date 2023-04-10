namespace Fit.Abstraction;

public interface IAssertor
{
    Task AssertAsync(IStateClaims stateClaims);
}
