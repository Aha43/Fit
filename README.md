*WORK IN PROGRESS*

# Fluent Integration Test (FIT) Framework

FIT is a .NET integration test framework where a *case* (i.e. usecases) is defined by a sequence of *act*s that acts on the system being tested when a *case* is run.
Unlike unit tests *act*s in a case are not independent: An *act* start acting on the system in a state caused by the *act*s that acted before it in the *case*. *Act*s are defined by the interface `IActor`.
The implementation of the interface can be used to perform many acts both in a *case* and in more than one *case*.
Implementations of the `IACtor` interface not only act on the system being testet but make claims about (writing to an instance of `SystemClaims`) the system state after the act.
After an *act* have acted the claims are checked to be true by implementations of the `IAssertor` interface.
Separating *asserting* from *acting* means also side effects can be caught not only that an *act* did work as expected.
Both `IActor` and `IAssertor` gets the part of the system they need to access through dependency injection.

## Examples

In next sections are code examples from demo projects found [here](https://github.com/Aha43/Fit/tree/main/src/Demo). Note that Fit is intended to be used from a unit test project, currently it is recommended to use XUnit framework since **Fit** provide support for that through the [Fit.Xunit](https://github.com/Aha43/Fit/tree/main/src/Fit.XUnit) project but looking at the sample code here and Fit.Xunit code using another framework similar to XUnit shold be easy. An issue #3 has been made to provide NUnit support in the future.  

### Defining cases

*Cases* can be defined by a class that implements the interface `ICaseDefiner`:

```cs
using Fit.Abstraction.Api;
using Fit.Abstraction.Client;
using Fit.Demo.Test.Actor;

namespace Fit.Demo.Test;

public class ToDoAndTagCase : ICaseDefiner
{
    public void AddCases(IFit fit)
    {
        fit.First<AddToDo>().With("Name", "TestToDoItem1").And("State", "Next")
            .Then<AddToDo>().With("Name", "TestToDoItem2").And("State", "SAP")
            .Then<AddTag>().With("Name", "Tag1")
            .Then<AddTag>().With("Name", "Tag2")
            .Then("RemoveTag").With("Name", "Tag1")
            .AsCase("ToDoAndTag");
    }

}
```
As seen in the example a fluent syntax is used to define a *case*. 
Even not knowing anything about the system being testet one learn quite a lot about it by reading this *case* code: It is a system where it is possible to add todo items and tags.
The parameter `fit` of type `IFit` is the API to define (what we do in this example) cases and run cases (later examples). 
`Do` methods tell that a named `IActor` is to *act* on the system *with* some parameters provided with the `With` method. 

### Implementing IActor classes that act and make claims

```cs
using Fit.Abstraction;
using Fit.Demo.Business;
using Fit.Demo.Domain;

namespace Fit.Demo.Test.Actor;

public class AddToDo : IActor
{
    private readonly ToDosViewController _toDoViewController;

    public AddToDo(ToDosViewController toDoViewController) => _toDoViewController = toDoViewController;

    public async Task ActAsync(ActorContext context)
    {
        var name = context.Parameters?.Get<string>("Name");

        await _toDoViewController.LoadAsync();
        _toDoViewController.NewToDo.Name = name;
        var created = await _toDoViewController.CreateToDo();

        context.StateClaims.ExpectedItemList<ToDo>().Add(created with { }); 
    }

}

```

### Implementing IAssertor classes that check claims made by actors

### Runing cases with the XUnit

#### Method 1

#### Method 2

