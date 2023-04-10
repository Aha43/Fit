using Fit.Abstraction.Api;

namespace Fit.Implementation;

internal class RunMode : IRunMode
{
    public bool IgnoreMissingActors { get; set; } = false;
    public bool Proto { get; set; } = false;
}
