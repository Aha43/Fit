using Fit.Demo.Business;

namespace Fit.Demo.Test.Actor;

public class AddToDoActor : IActor
{
    private readonly ToDoViewController _toDoViewController;

    public AddToDoActor(ToDoViewController toDoViewController) => _toDoViewController = toDoViewController;

    public Task ActAsync(TypedMap stateClaims, TypedMap parameters)
    {
        throw new NotImplementedException();
    }

}
