namespace fit
{
    public interface IAsserter
    {
        Task AssertAsync(TypedMap systemClaims);
    }
}
