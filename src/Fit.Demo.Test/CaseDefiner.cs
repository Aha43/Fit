using Fit.Abstraction;
using Fit.Demo.Test.Actor;

namespace Fit.Demo.Test;

public class CaseDefiner : ICaseDefiner
{
    public void AddCases(Fit fit)
    {
        fit.Do("Login").With("Name", "Arne").AsSegment("Login");
        fit.Do("SaveWork").Do("Logout").AsSegment("Exit");

        fit.FromStart("Login")
            .Do<AddToDo>().With("Name", "TestToDoItem1")
            .Do<AddToDo>().With("Name", "TestToDoItem2")
            .Do<AddTag>().With("Name", "Tag1")
            .Do<AddTag>().With("Name", "Tag2")
            .Do("RemoveTag").With("Name", "Tag1")
            .ContinueWith("Exit")
            .ContinueWith("Login")
            .AsCase("FirstCase");
    }

}
