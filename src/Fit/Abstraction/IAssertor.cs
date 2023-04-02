namespace Fit.Abstraction;

public interface IAssertor
{
    Task AssertAsync(StateClaims stateClaims);
}
