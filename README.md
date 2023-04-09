# Fluent Integration Test (FIT) Framework

# WORK IN PROGRESS

FIT is a .NET integration test framework where a *case* (i.e. usecases) is defined by a sequence of *act*s that acts on the system being tested when a *case* is run.
Unlike unit tests *act*s in a case are not independent: An *act* start acting on the system in a state caused by the *act*s that acted before it in the *case*. *Act*s are defined by the interface `IActor`.
The implementation of the interface can be used to perform many acts both in a *case* and in more than one *case*.
Implementations of the `IACtor` interface not only act on the system being testet but make claims about (writing to an instance of `SystemClaims`) the system state after the act.
After an *act* have acted the claims are checked to be true by implementations of the `IAssertor` interface.
Separating *asserting* from *acting* means also side effects can be caught not only that an *act* did work as expected.
Both `IActor` and `IAssertor` gets the part of the system they need to access through dependency injection.

## Examples

### Defining cases

*Cases* can be defined by a class that implements the interface `ICaseDefiner`:

```cs
using Fit.Abstraction;
using Fit.Demo.Test.Actor;

namespace Fit.Demo.Test;

public class CaseDefiner : ICaseDefiner
{
    public void AddCases(IFit fit)
    {
        fit.First<AddToDo>().With("Name", "TestToDoItem1")
            .Then<AddToDo>().With("Name", "TestToDoItem2").And("State", "Next")
            .Then<AddTag>().With("Name", "Tag1")
            .Then<AddTag>().With("Name", "Tag2")
            .Then("RemoveTag").With("Name", "Tag1")
            .AsCase("FirstCase");
    }

}
```
As seen in the example a fluent syntax is used to define a *case*. 
Even not knowing anything about the system being testet one learn quite a lot about it by reading this *case* code: It is a system where it is possible to add todo items and tags.
The parameter `fit` of type `IFit` is the API to define (what we do in this example) cases and run cases (later examples). 
`Do` methods tell that a named `IActor` is to *act* on the system *with* some parameters provided with the `With` method. 

### Implementing Actor classes

### Implementing

### Runing cases with XUnit

#### Method 1

#### Method 2
 


# WORK IN PROGRESS
