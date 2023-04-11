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
