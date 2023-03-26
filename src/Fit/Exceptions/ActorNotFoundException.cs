namespace Fit.Exceptions;

public class ActorNotFoundException : Exception
{
    public ActorNotFoundException(string name) : base(name) { }
}
