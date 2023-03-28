namespace Fit.Exceptions;

public class DuplicateActorException : Exception
{
    internal DuplicateActorException(string name) : base(name) { }
}
