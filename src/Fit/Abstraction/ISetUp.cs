namespace Fit.Abstraction;

public interface ISetUp
{
    Task SetUpAsync(IStateClaims stateClaims);
}
