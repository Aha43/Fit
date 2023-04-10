using Fit.Demo.Specification;

namespace Fit.Demo.Business;

public class ToDoViewController
{
    private IToDoApi _toDoApi;
    private ITagApi _tagApi;

    

    public ToDoViewController(
        IToDoApi toDoApi,
        ITagApi tagApi)
    {
        _toDoApi = toDoApi;
        _tagApi = tagApi;
    }

}
