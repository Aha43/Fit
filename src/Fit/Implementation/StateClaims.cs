using Fit.Abstraction.Api;

namespace Fit.Implementation;

internal class StateClaims : Dictionary<string, object>, IStateClaims
{
    public T Get<T>(string key) => (T)this[key];
}
