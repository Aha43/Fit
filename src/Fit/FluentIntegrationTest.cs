﻿using Fit.Abstraction.Api;
using Fit.Implementation;

namespace Fit;

public static class FluentIntegrationTest
{
    public static IFit Create(Action<IFitOptions>? o = null) => new FitImplementation(o);
}
