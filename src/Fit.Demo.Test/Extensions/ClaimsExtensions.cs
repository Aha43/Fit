using Fit.Demo.Domain;

namespace Fit.Demo.Test.Extensions;

public static class ClaimsExtensions
{
    public static List<ToDo> ExpectedToDoList(this TypedMap claims)
    {
        var retVal = claims.Get<List<ToDo>>(nameof(ExpectedToDoList));
        if (retVal == null) 
        {
            retVal = new();
            claims.Set(nameof(ExpectedToDoList), retVal);
        }
        return retVal;
    }

}
