﻿using Fit.Demo.Business;
using Fit.Demo.Infrastructure.InMemory;
using Fit.XUnit;

namespace Fit.Demo.Test;

public class TestSource : FitXunitTestSource
{
    public TestSource() : base(o =>
    {
        o.Services.AddFitDemoInMemoryInfrastructure()
            .AddFitDemoBusiness();
    }) { }
}
