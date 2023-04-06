namespace Fit.Exceptions;

public class ActorNotFoundException : Exception
{
    internal ActorNotFoundException(string name1, string name2) : base($"No IActor named '{name1}' or '{name2}' found") { }
}
