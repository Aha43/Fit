namespace Fit
{
    public static class Util
    {
        public static IEnumerable<Type> FindNonAbstractTypes<T>() where T : class
        {
            var tType = typeof(T);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => !type.IsAbstract &&
                    !type.IsInterface &&
                    tType.IsAssignableFrom(type));
            return types;
        }

    }

}
