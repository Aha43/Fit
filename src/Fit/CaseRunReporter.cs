using System.Text;

namespace Fit;

public class CaseRunReporter
{
    private readonly List<CaseReport> _caseReports = new();

    private CaseReport? _current;

    public void CaseStart(string name)
    {
        _current = new CaseReport(name);
        _caseReports.Add(_current);
    }

    public void ActorPerforms(ActorContext ctx, bool notRun, bool exists) 
    { 
        _current?.ActorReport.Add(new ActorReport { ActorName = ctx.ActorName, NotRun = notRun, Exists = exists, Report = ctx.Parameters?.ToString() });
    }

    public CaseReport? GetCaseRaport(string name) => _caseReports.Where(e => e.CaseName == name).FirstOrDefault();

    public override string ToString()
    {
        var sb = new StringBuilder();
        foreach (var caseReport in _caseReports) sb.AppendLine(caseReport.ToString());
        return sb.ToString();
    }

}

public class CaseReport
{
    public string CaseName { get; }
    public List<ActorReport> ActorReport { get; } = new();
    public CaseReport(string caseName) => CaseName = caseName;
    public string ToString(StringBuilder sb)
    {
        sb.AppendLine($"Case: '{CaseName}'");
        foreach (var ar in ActorReport) sb.AppendLine($"  {ar}");
        return sb.ToString();
    }
    public override string ToString() => ToString(new StringBuilder());
}

public class ActorReport
{
    public required string? ActorName { get; init; }
    public required bool NotRun { get; init; }
    public required string? Report { get; init; }
    public required bool Exists { get; init; }
    internal string ExistsStatement => !Exists ? " - Actor not implemented" : "";
    internal string RunStatement => NotRun ? " - Not run" : "";
    public override string ToString() => $"Actor '{ActorName}' performed {Report}{ExistsStatement}{RunStatement}";
}
