namespace Fit.Exceptions;

public class DuplicateTestException : Exception
{
    internal DuplicateTestException(string name) : base(name) { }
}
