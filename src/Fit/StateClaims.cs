using Fit.Abstraction;

namespace Fit;

internal class StateClaims : Dictionary<string, object>, IStateClaims
{
    public T Get<T>(string key) => (T)this[key];
}
