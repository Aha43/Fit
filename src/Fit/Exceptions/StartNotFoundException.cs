namespace Fit.Exceptions;

public class StartNotFoundException : Exception
{
    internal StartNotFoundException(string name) : base(name) { }
}
