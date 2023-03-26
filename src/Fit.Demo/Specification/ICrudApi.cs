namespace Fit.Demo.Specification;

public interface ICrudApi<T, C, R, U, D>
{
    Task<T> CreateAsync(C p, CancellationToken ct);
    Task<IEnumerable<T>> ReadAsync(R p, CancellationToken ct);
    Task<T?> UpdateAsync(U p, CancellationToken ct);
    Task<T?> DeleteAsync(D d, CancellationToken ct);
}
