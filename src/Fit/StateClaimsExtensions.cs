﻿using Fit.Exceptions;

namespace Fit;

public static class StateClaimsExtensions
{
    public static List<T> ExpectedItemList<T>(this StateClaims claims) where T : class
    {
        var name = $"{typeof(T).Name}ExpectedList";

        if (claims.TryGetValue(name, out object? value))
        {
            if (value is not List<T> retVal)
            {
                throw new StateClaimWrongTypeExceptions(name);
            }

            return retVal;
        }

        var list = new List<T>();
        claims[name] = list;
        return list;
    }

}
