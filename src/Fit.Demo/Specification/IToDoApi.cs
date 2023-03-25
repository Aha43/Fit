using Fit.Demo.Domain;

namespace Fit.Demo.Specification;

public interface IToDoApi : ICrudApi<ToDo, ToDo, int?, ToDo, int> { }
