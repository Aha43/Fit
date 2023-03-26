using Fit.Demo.Business;
using Fit.Demo.Domain;
using Fit.Demo.Test.Extensions;

namespace Fit.Demo.Test.Actor;

public class AddTag : IActor
{
    private readonly TagsViewController _tagsViewController;

    public AddTag(TagsViewController toDoViewController) => _tagsViewController = toDoViewController;

    public async Task ActAsync(StateClaims stateClaims, ActorParameters parameters)
    {
        var name = parameters.Get<string>("Name");

        await _tagsViewController.LoadAsync();
        _tagsViewController.NewTag.Name = name;
        var created = await _tagsViewController.CreateTag();

        stateClaims.ExpectedItemList<Tag>().Add(created with { });
    }
}
