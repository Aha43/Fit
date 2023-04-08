namespace Fit.Exceptions;

public class DuplicateSegmentException : Exception
{
    internal DuplicateSegmentException(string name) : base(name) { }
}
