namespace Fit
{
    internal static class InstantiateUtil
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

        internal static IEnumerable<T> FindAndInstantiate<T>() where T : class
        {
            var types = FindNonAbstractTypes<T>();
            var retVal = Instantiate<T>(types);
            return retVal;
        }

        internal static IEnumerable<T> Instantiate<T>(IEnumerable<Type> types) where T : class
        {
            var retVal = new List<T>();
            foreach (var type in types) 
            {
                if (Activator.CreateInstance(type) is T t)
                {
                    retVal.Add(t);
                }
            }
            return retVal;
        }

    }

}
