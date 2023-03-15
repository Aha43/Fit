namespace Fit
{
    public abstract class AssertorBase
    {
        protected AssertorBase(IServiceProvider serviceProvider) => Initialize(serviceProvider);

        protected abstract void Initialize(IServiceProvider serviceProvider);

        public abstract Task AssertAsync(TypedMap systemClaims);
    }
}
