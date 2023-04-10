using Fit.Abstraction.Api;

namespace Fit.Implementation;

internal class RunMode : IRunMode
{
    /// <summary>
    /// If true will not throw exception if actor class not found, usefull for tdd strategy. Default is false.
    /// </summary>
    public bool IgnoreMissingActors { get; set; } = false;

    /// <summary>
    /// If true will not actually act (run actors). Default is false.
    /// </summary>
    public bool Proto { get; set; } = false;
}
