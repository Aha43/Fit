using Fit.Demo.Business;
using Fit.Demo.Infrastructure.InMemory;

namespace Fit.Demo.Test;

public class UnitTest
{
    private readonly FitManager _fit;

    public UnitTest() 
    {
        _fit = new FitManager(o => { 
            o.Services.AddFitDemoInMemoryInfrastructure()
                .AddFitDemoBusiness();
        });

        _fit.Do("AddToDo").AsCase("FirstCase");
    }


    [Fact]
    public async Task Test()
    {
        await _fit.RunTest("FirstCase");
    }

}