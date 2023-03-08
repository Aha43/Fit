namespace fit;

public interface IActor
{
    Task ActAsync(TypedMap stateClaims, TypedMap parameters);
}
