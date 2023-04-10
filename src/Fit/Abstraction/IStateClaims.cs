namespace Fit.Abstraction;

public interface IStateClaims : IDictionary<string, object>
{
    public T Get<T>(string key);
}
