namespace Fit;

public interface IActor
{
    Task ActAsync(TypedMap stateClaims, TypedMap parameters);
}
