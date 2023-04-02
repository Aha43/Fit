namespace Fit.Abstraction;

public interface ITearDown
{
    Task TearDownAsync(StateClaims stateClaims);
}
