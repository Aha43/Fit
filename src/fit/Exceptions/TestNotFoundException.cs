namespace fit.Exceptions
{
    public class TestNotFoundException : Exception
    {
        public TestNotFoundException(string name) : base(name) { }
    }
}
