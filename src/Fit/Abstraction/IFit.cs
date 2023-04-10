namespace Fit.Abstraction;

public interface IFit
{
    IActorNode First<T>() where T : class;
    IActorNode First(string name);
    IActorNode FirstDo(string name);
    IEnumerable<string> CaseNames { get; }
    Task<string> RunCase(string caseName);
}
