namespace Fit;

public interface IAssertor
{
    Task AssertAsync(TypedMap systemClaims);
}
