namespace Fit.Exceptions; 

public class CaseNotFoundException : Exception 
{
    internal CaseNotFoundException(string name) : base(name) { }
}
