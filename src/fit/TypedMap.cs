using fit.Exceptions;

namespace fit;

public class TypedMap
{
    private readonly Dictionary<string, object> _maps = new();

    public void Set<T>(string name, T? value)
    {
        var typeName = typeof(T).Name;

        Dictionary<string, T?>? dict;
        if (_maps.TryGetValue(typeName, out object? map)) 
        { 
            dict = map as Dictionary<string, T?>;
            if (dict != null) 
            {
                dict[name] = value;
                return;
            }

            throw new TypedMapWrongTypeExceptions(name);
        }

        dict = new Dictionary<string, T?> { [name] = value };
        _maps[typeName] = dict;
    }

    public T? Get<T>(string name) 
    {
        var typeName = typeof(T).Name;

        Dictionary<string, T?>? dict;
        if (_maps.TryGetValue(typeName, out object? map))
        {
            dict = map as Dictionary<string, T?>;
            if (dict != null)
            {
                if (dict.TryGetValue(name, out T? value)) return value;
                return default;
            }

            throw new TypedMapWrongTypeExceptions(name);
        }

        return default;
    }

    public void Clear() => _maps.Clear();

    public void Clear<T>()
    {
        var typeName = typeof(T).Name;

        Dictionary<string, T?>? dict;
        if (_maps.TryGetValue(typeName, out object? map))
        {
            dict = map as Dictionary<string, T?>;
            if (dict != null)
            {
                dict.Clear();
                return;
            }

            throw new Exception("not right type");
        }
    }

}
