namespace Fit.Abstraction.Api;

public interface IRunMode
{
    bool IgnoreMissingActors { get; set; }
    bool Proto { get; set; }
}
