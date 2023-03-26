using Fit.Demo.Business;
using Fit.Demo.Infrastructure.InMemory;
using Fit.Demo.Test.Actor;

namespace Fit.Demo.Test;

public class UnitTest
{
    private readonly Fit _fit;

    public UnitTest() 
    {
        _fit = new Fit(o => { 
            o.Services.AddFitDemoInMemoryInfrastructure()
                .AddFitDemoBusiness();
        });

        _fit.Do<AddToDo>().AsCase("FirstCase");
    }


    [Fact]
    public async Task Test()
    {
        await _fit.RunTest("FirstCase");
    }

}