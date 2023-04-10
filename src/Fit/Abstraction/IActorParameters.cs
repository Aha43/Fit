namespace Fit.Abstraction;

public interface IActorParameters
{
    T Get<T>(string key);
}
