﻿using Fit.Abstraction.Api;
using Fit.Abstraction.Client;
using Fit.Demo.Test.Actor;

namespace Fit.Demo.Test;

public class CaseDefiner : ICaseDefiner
{
    public void AddCases(IFit fit)
    {
        fit.First("Login").With("Name", "Arne").AsSegment("Login");
        fit.First("SaveWork").Then("Logout").AsSegment("Exit");

        fit.FirstDo("Login")
            .Then<AddToDo>().With("Name", "TestToDoItem1").And("State", "Next")
            .Then<AddToDo>().With("Name", "TestToDoItem2").And("State", "SAP")
            .Then<AddTag>().With("Name", "Tag1")
            .Then<AddTag>().With("Name", "Tag2")
            .Then("RemoveTag").With("Name", "Tag1")
            .ThenContinueWith("Exit")
            .ThenContinueWith("Login")
            .AsCase("FirstCase");
    }

}
