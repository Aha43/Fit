namespace Fit.Exceptions;

public class SegmentNotFoundException : Exception
{
    internal SegmentNotFoundException(string name) : base(name) { }
}
