using Fit.Abstraction.Api;
using System.Collections;

namespace Fit.XUnit;

public class FitXunitTestSource : IEnumerable<object[]>
{
    private readonly IFit _fit;

    protected FitXunitTestSource(Action<IFitOptions>? o = null) => _fit = FluentIntegrationTest.Create(o);

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var caseName in _fit.CaseNames) yield return new object[] { _fit, caseName };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
