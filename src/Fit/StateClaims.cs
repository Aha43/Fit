namespace Fit;

public class StateClaims : Dictionary<string, object>
{
    public T Get<T>(string key) => (T)this[key];
}
