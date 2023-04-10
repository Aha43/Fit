namespace Fit.Abstraction;

public interface ITearDown
{
    Task TearDownAsync(IStateClaims stateClaims);
}
