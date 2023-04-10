namespace Fit.Abstraction.Api;

public interface IStateClaims : IDictionary<string, object>
{
    public T Get<T>(string key);
}
