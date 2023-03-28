namespace Fit.Exceptions; 

public class TestNotFoundException : Exception 
{
    internal TestNotFoundException(string name) : base(name) { }
}
