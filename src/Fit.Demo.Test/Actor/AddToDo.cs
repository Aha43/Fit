using Fit.Demo.Business;
using Fit.Demo.Test.Extensions;

namespace Fit.Demo.Test.Actor;

public class AddToDo : IActor
{
    private readonly ToDoViewController _toDoViewController;

    public AddToDo(ToDoViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task ActAsync(TypedMap stateClaims, TypedMap parameters)
    {
        var name = parameters.Get<string>("Name");

        await _toDoViewController.LoadAsync();
        _toDoViewController.NewToDo.Name = name;
        var created = await _toDoViewController.CreateToDo();

        stateClaims.ExpectedToDoList().Add(created with { });
    }

}
