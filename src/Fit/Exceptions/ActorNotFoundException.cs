namespace Fit.Exceptions;

public class ActorNotFoundException : Exception
{
    internal ActorNotFoundException(string name) : base(name) { }
}
