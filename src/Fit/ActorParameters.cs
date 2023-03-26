namespace Fit;

public class ActorParameters : Dictionary<string, object>
{
    public T Get<T>(string key) => (T)this[key];
}
