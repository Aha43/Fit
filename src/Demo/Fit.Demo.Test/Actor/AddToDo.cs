using Fit.Abstraction.Api;
using Fit.Abstraction.Client;
using Fit.Demo.Business;
using Fit.Demo.Domain;
using Fit.ExtensionMethods;

namespace Fit.Demo.Test.Actor;

public class AddToDo : IActor
{
    private readonly ToDosViewController _toDoViewController;

    public AddToDo(ToDosViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task ActAsync(IActorContext context)
    {
        var name = context.Parameters.Get<string>("Name");

        await _toDoViewController.LoadAsync();
        _toDoViewController.NewToDo.Name = name;
        var created = await _toDoViewController.CreateToDo();

        context.StateClaims.ExpectedItemList<ToDo>().Add(created with { });
    }

}
