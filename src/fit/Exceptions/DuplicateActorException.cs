namespace Fit.Exceptions;

public class DuplicateActorException : Exception
{
    public DuplicateActorException(string name) : base(name) { }
}
