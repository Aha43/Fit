using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

internal class ActorContext : IActorContext
{
    public string CaseName { get; }

    public string? ActorName { get; internal set; }

    public StateClaims StateClaims { get; } = new();

    private IActorParameters? _parameters;
    public IActorParameters Parameters 
    {     
        get
        {
            if (_parameters == null)
            {
                throw new InternalFitException("Parameters not set in ActorContext");
            }

            return _parameters;
        }
        internal set
        {
            _parameters = value;
        }
    }

    internal ActorContext(string caseName) => CaseName = caseName; 
}
