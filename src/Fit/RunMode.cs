using Fit.Abstraction;

namespace Fit;

internal class RunMode : IRunMode
{
    public bool IgnoreMissingActors { get; set; } = false;
    public bool Proto { get; set; } = false;    
}
