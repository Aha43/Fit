using Fit.Demo.Specification;

namespace Fit.Demo.Infrastructure.InMemory;

public class InMemoryBase<T> : ICrudApi<T, T, int?, T, int> where T : class
{
    private int _nextId = 1;

    private readonly Dictionary<int, T> _items = new();

    public Task<T> CreateAsync(T p, CancellationToken ct)
    {
        var id = _nextId++;
        SetId(p, id);
        _items[id] = p;
        return Task.FromResult(p);
    }

    public Task<IEnumerable<T>> ReadAsync(int? id, CancellationToken ct)
    {
        if (id == null) return Task.FromResult(_items.Values.AsEnumerable());

        var retVal = _items[id.Value];
        if (retVal == null) return Task.FromResult(Enumerable.Empty<T>());
        return Task.FromResult(new T[] { retVal }.AsEnumerable());
    }

    public Task<T?> UpdateAsync(T p, CancellationToken ct)
    {
        int id = GetId(p);
        if (!_items.ContainsKey(id)) Task.FromResult<T?>(null);
        _items[id] = p;
        return Task.FromResult<T?>(p);
    }

    public Task<T?> DeleteAsync(int id, CancellationToken ct)
    {
        if (!_items.ContainsKey(id)) Task.FromResult<T?>(null);
        var retVal = _items[id];
        return Task.FromResult<T?>(retVal);
    }

    private static int SetId(T item, int id) => ((dynamic)item).Id = id;
    private static int GetId(T item) => ((dynamic)item).Id;


}
