﻿using Fit.Abstraction.Api;
using Fit.Abstraction.Client;
using Fit.Demo.Business;
using Fit.Demo.Domain;
using Fit.ExtensionMethods;

namespace Fit.Demo.Test.Assertor;

public class ExpectedToDoListAssertor : IAssertor
{
    private readonly ToDosViewController _toDoViewController;

    public ExpectedToDoListAssertor(ToDosViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task AssertAsync(IStateClaims systemClaims)
    {
        await _toDoViewController.LoadAsync();

        var expected = systemClaims.ExpectedItemList<ToDo>();

        Assert.Equal(expected.Count, _toDoViewController.ToDos.Count);
        for (var i = 0; i < expected.Count; i++ ) 
        {
            var expectedToDo = expected[i];
            var actualToDo = _toDoViewController.ToDos[i];
            Assert.True(expectedToDo == actualToDo);
        }
    }

}
