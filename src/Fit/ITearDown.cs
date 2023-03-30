namespace Fit;

public interface ITearDown
{
    Task TearDownAsync(StateClaims stateClaims);
}
