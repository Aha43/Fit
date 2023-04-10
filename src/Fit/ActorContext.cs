using Fit.Abstraction;
using Fit.Exceptions;

namespace Fit;

internal class ActorContext : IActorContext
{
    public string CaseName { get; }

    internal ActorContext(string caseName) => CaseName = caseName;

    public string? ActorName { get; internal set; }

    public IStateClaims StateClaims { get; } = new StateClaims();

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

     
}
