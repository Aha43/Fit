using Fit.Demo.Business;
using Fit.Demo.Domain;

namespace Fit.Demo.Test.Actor;

public class AddToDo : IActor
{
    private readonly ToDosViewController _toDoViewController;

    public AddToDo(ToDosViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task ActAsync(StateClaims stateClaims, ActorParameters parameters)
    {
        var name = parameters.Get<string>("Name");

        await _toDoViewController.LoadAsync();
        _toDoViewController.NewToDo.Name = name;
        var created = await _toDoViewController.CreateToDo();

        stateClaims.ExpectedItemList<ToDo>().Add(created with { });
    }

}
