using Fit.Abstraction;
using Fit.Demo.Business;
using Fit.Demo.Domain;

namespace Fit.Demo.Test.Actor;

public class AddTag : IActor
{
    private readonly TagsViewController _tagsViewController;

    public AddTag(TagsViewController toDoViewController) => _tagsViewController = toDoViewController;

    public async Task ActAsync(IActorContext context)
    {
        var name = context.Parameters.Get<string>("Name");

        await _tagsViewController.LoadAsync();
        _tagsViewController.NewTag.Name = name;
        var created = await _tagsViewController.CreateTag();

        context.StateClaims.ExpectedItemList<Tag>().Add(created with { });
    }
}
