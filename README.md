*WORK IN PROGRESS*

# Fluent Integration Test (FIT) Framework

FIT is a .NET integration test framework where a *case* (i.e. as in use cases) is defined by a sequence of *act*s that acts on the *system* being tested when a *case* is run.
Unlike unit tests the *act*s in a *case* are not independent: An *act* start acting on the *system* in a state caused by the *act*s that acted before it in the *case*. *Act*s are defined by the interface `IActor`.
An implementation of the interface can be used to perform many *acts* both in a *case* and in more than one *case*.
Implementations of the `IACtor` interface not only *act* on the *system* being testet but make *claims* about (by writing to an instance of `ISystemClaims`) the *system* *state* after the *act*.
After an *act* have acted the *claims* are checked to be true by implementations of the `IAssertor` interface (by reading *claims* from an instance of `ISystemClaims` and comparing *claim* values with the *state* read from the *system* itself).
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
`First` and `Then` methods tell that a named `IActor` is to *act* on the system *with* some parameters provided with the `With` and `And` methods. 
Note that the methods that tell an `IActor` implementation to *act* comes in two forms:

- Identifying the actor with a generic type argument as in `First<AddToDo>` and `Then<AddTag>`. Using these methods the `IActor` implementation **must** exist.
- Identifying the actor pasing the class name as a parameter as in `First("AddToDo")` and `Then("AddTag")`. Using these methods the `IActor` implementation may not yet exist if run in a *IgnoreMissingActors* mode (shown in examples below). This is usefull in a *test driven implementation strategy*. *Tip:* If using this strategy consider to first use this form and then when actors get implemented switch to the generic parameter form to make it easy to read from code what has been implemented and what is planned.

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

### Runing cases

Fit does not implement any test runners but utilize unit test framework runners so to get IDE support and be used in a automatic testing scenario. The basic idea is that each *case* is run as one test. 
The following examples show use of the XUnit framework.

#### Method 1
```cs 
using Fit.Abstraction.Api;
using Fit.Demo.Business;
using Fit.Demo.Infrastructure.InMemory;

namespace Fit.Demo.Test;

public class DemoTest_Method1
{
    private readonly IFit _fit = FluentIntegrationTest.Create(o =>
    {
        o.RunMode.Proto = false;
        o.RunMode.IgnoreMissingActors = true;
        o.Services.AddFitDemoInMemoryInfrastructure()
            .AddFitDemoBusiness();
    });

    [Theory]
    [InlineData("ToDoAndTag")]
    public async Task Test(string caseName)
    {
        await _fit.RunCase(caseName);
    }

}
```

In this example an instance of the Fit api is created using the static method `FluentIntegrationTest.Create` that accept a function that receives an option object to set:
- `Boolean` property `RunMode.Proto` that if true will not actual perform any acts. Default value is false.
- `Boolean` property `RunMode.IgnoreMissingActors` that if is true will not throw an exception if an actor is named but implementation is not found.
- `IServiceCollection` property `Services` to register the services that makes up the system being testet. 

A single test method is then defined to run given cases, the idea is to use XUnit's `Theory` and `InlineData` attributes to list all the cases to run.

##### Pro / cons

#### Method 2
```cs 
using Fit.Abstraction.Api;
using Fit.XUnit;
using Xunit.Abstractions;

namespace Fit.Demo.Test;

public class DemoTest_Method2 : FitXunitTestBase
{
    public DemoTest_Method2(ITestOutputHelper testReporter) : base(testReporter) { }

    [Theory]
    [ClassData(typeof(TestSource))]
    public async Task RunCases(IFit fit, string name) => await RunNamedCase(fit, name);
}
```

this depends on that we have implemented the source class feeding the *cases*:
```cs
using Fit.Demo.Business;
using Fit.Demo.Infrastructure.InMemory;
using Fit.XUnit;

namespace Fit.Demo.Test;

public class TestSource : FitXunitTestSource
{
    public TestSource() : base(o =>
    {
        o.RunMode.Proto = true;
        o.RunMode.IgnoreMissingActors = true;
        o.Services.AddFitDemoInMemoryInfrastructure()
            .AddFitDemoBusiness();
    }) { }
}
```

### Advanced use

#### Defining and using act segments

#### Set up and tear down


