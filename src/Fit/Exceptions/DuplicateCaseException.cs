namespace Fit.Exceptions;

public class DuplicateCaseException : Exception
{
    internal DuplicateCaseException(string name) : base(name) { }
}
