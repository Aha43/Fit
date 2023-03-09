namespace fit.Exceptions
{
    public class DuplicateTestException : Exception
    {
        public DuplicateTestException(string name) : base(name) { }
    }
}
