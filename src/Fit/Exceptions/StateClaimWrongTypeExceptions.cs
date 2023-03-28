namespace Fit.Exceptions;

public class StateClaimWrongTypeExceptions : Exception
{
    internal StateClaimWrongTypeExceptions(string name) : base($"Value named '{name}' is not of expected of type") { }
}
