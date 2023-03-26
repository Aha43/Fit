using Fit.Demo.Domain;

namespace Fit.Demo.Test.Extensions;

public static class ClaimsExtensions
{
    public static List<T> ExpectedItemList<T>(this StateClaims claims) where T : class
    {
        var name = $"{typeof(T).Name}ExpectedList";

        if (claims.TryGetValue(name, out object? value))
        {
            var retVal = value as List<T>;
            if (retVal == null)
            {
                throw new ArgumentException("type error");
            }

            return retVal;
        }

        var list = new List<T>();
        claims[name] = list;
        return list;
    }

}
