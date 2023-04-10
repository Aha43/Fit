using Fit.Abstraction.Api;
using Fit.Abstraction.Client;
using Fit.Demo.Business;
using Fit.Demo.Domain;
using Fit.ExtensionMethods;

namespace Fit.Demo.Test.Assertor;

public class ExpectedTagListAssertor : IAssertor
{
    private readonly TagsViewController _tagsViewController;

    public ExpectedTagListAssertor(TagsViewController tagsViewController) => _tagsViewController = tagsViewController;

    public async Task AssertAsync(IStateClaims systemClaims)
    {
        await _tagsViewController.LoadAsync();

        var expected = systemClaims.ExpectedItemList<Tag>();

        Assert.Equal(expected.Count, _tagsViewController.Tags.Count);
        for (var i = 0; i < expected.Count; i++ ) 
        {
            var expectedTag = expected[i];
            var actualTag = _tagsViewController.Tags[i];
            Assert.True(expectedTag == actualTag);
        }
    }

}
