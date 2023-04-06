namespace Fit.Exceptions;

public class DuplicateStartException : Exception
{
    internal DuplicateStartException(string name) : base(name) { }
}
