namespace fit.Exceptions;

public class TypedMapWrongTypeExceptions : Exception
{
    public TypedMapWrongTypeExceptions(string name) : base($"Value named '{name}' is not of expected of type") { }
}
