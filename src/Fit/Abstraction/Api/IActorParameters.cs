namespace Fit.Abstraction.Api;

public interface IActorParameters
{
    T Get<T>(string key);
}
