namespace Fit
{
    public abstract class ActorBase
    {
        protected ActorBase(IServiceProvider serviceProvider) => Initialize(serviceProvider);

        protected abstract void Initialize(IServiceProvider serviceProvider);

        public abstract Task ActAsync(TypedMap stateClaims, TypedMap parameters);
    }

}
