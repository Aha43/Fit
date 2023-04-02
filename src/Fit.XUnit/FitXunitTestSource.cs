using System.Collections;

namespace Fit.XUnit;

public class FitXunitTestSource : IEnumerable<object[]>
{
    private readonly Fit _fit;

    protected FitXunitTestSource(Action<FitOptions>? o = null) => _fit = new Fit(o);

    public IEnumerator<object[]> GetEnumerator()
    {
        foreach (var caseName in _fit.CaseNames) yield return new object[] { _fit, caseName };
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
