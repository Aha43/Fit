using System.Text;

namespace Fit;

public class ActorParameters : Dictionary<string, object>
{
    public T Get<T>(string key) => (T)this[key];

    public override string ToString()
    {
        var sb = new StringBuilder();

        var first = true;
        foreach (var item in this )
        {
            if (!first) sb.Append(" and ");
            else sb.Append("with ");
            sb.Append($"{item.Key} is {item.Value}");
        }

        return sb.ToString();
    }
}
