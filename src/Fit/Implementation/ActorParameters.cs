using Fit.Abstraction;
using System.Text;

namespace Fit.Implementation;

internal class ActorParameters : IActorParameters
{
    private readonly IDictionary<string, object> _parameters;

    internal ActorParameters(IDictionary<string, object> parameters) => _parameters = parameters;

    public T Get<T>(string key) => (T)_parameters[key];

    public override string ToString()
    {
        var sb = new StringBuilder();

        var first = true;
        foreach (var item in _parameters)
        {
            if (!first) sb.Append(" and ");
            else sb.Append("with ");
            first = false;
            sb.Append($"{item.Key} is {item.Value}");
        }

        return sb.ToString();
    }

}
