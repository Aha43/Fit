using Fit.Demo.Business;
using Fit.Demo.Test.Extensions;

namespace Fit.Demo.Test.Assertor;

public class ExpectedToDoListAssertor : IAssertor
{
    private readonly ToDoViewController _toDoViewController;

    public ExpectedToDoListAssertor(ToDoViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task AssertAsync(TypedMap systemClaims)
    {
        await _toDoViewController.LoadAsync();

        var expected = systemClaims.ExpectedToDoList();

        Assert.Equal(expected.Count, _toDoViewController.ToDos.Count);
        for (var i = 0; i < expected.Count; i++ ) 
        {
            var expectedToDo = expected[i];
            var actualToDo = _toDoViewController.ToDos[i];
            Assert.True(expectedToDo == actualToDo);
        }
    }

}
