using Fit.Abstraction;
using System.Text;

namespace Fit.Implementation;

internal class CaseRunReporter
{
    private readonly List<CaseReport> _caseReports = new();

    private CaseReport? _current;

    internal void CaseStart(string name)
    {
        _current = new CaseReport(name);
        _caseReports.Add(_current);
    }

    internal void ActorPerforms(IActorContext ctx, bool notRun, bool exists)
    {
        _current?.ActorReport.Add(new ActorReport { ActorName = ctx.ActorName, NotRun = notRun, Exists = exists, Report = ctx.Parameters?.ToString() });
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var caseReport in _caseReports) sb.AppendLine(caseReport.ToString());
        return sb.ToString();
    }

}

internal class CaseReport
{
    internal string CaseName { get; }
    internal List<ActorReport> ActorReport { get; } = new();
    internal CaseReport(string caseName) => CaseName = caseName;
    internal string ToString(StringBuilder sb)
    {
        sb.AppendLine($"Case: '{CaseName}'");
        foreach (var ar in ActorReport) sb.AppendLine($"  {ar}");
        return sb.ToString();
    }
    public override string ToString() => ToString(new StringBuilder());
}

internal class ActorReport
{
    internal required string? ActorName { get; init; }
    internal required bool NotRun { get; init; }
    internal required string? Report { get; init; }
    internal required bool Exists { get; init; }
    internal string ExistsStatement => !Exists ? " - Actor not implemented" : "";
    internal string RunStatement => NotRun ? " - Not run" : "";
    public override string ToString() => $"Actor '{ActorName}' performed {Report}{ExistsStatement}{RunStatement}";
}
