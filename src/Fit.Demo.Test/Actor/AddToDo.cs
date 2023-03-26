using Fit.Demo.Business;

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
        await _toDoViewController.CreateToDo();
    }

}
