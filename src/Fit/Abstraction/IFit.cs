namespace Fit.Abstraction;

public interface IFit
{
    IActorNode Do<T>() where T : class;
    IActorNode Do(string name);
    IActorNode FromStart(string name);
    IEnumerable<string> CaseNames { get; }
    Task RunCase(string caseName, CaseRunReporter? caseRunReporter = null);
}
