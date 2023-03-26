using Fit.Demo.Domain;
using Fit.Demo.Specification;
using Fit.Demo.Validation;

namespace Fit.Demo.Business;

public class TagsViewController
{
    private readonly ITagApi _api;

    public TagValidator ValidationRules { get; private set; }

    public TagsViewController(
        ITagApi api,
        TagValidator validationRules)
    {
        _api = api;
        ValidationRules = validationRules;
    }

    public List<Tag> Tags { get; } = new List<Tag>();

    public Tag NewTag { get; private set; } = new();

    public async Task LoadAsync()
    {
        var tags = await _api.ReadAsync(null, default);
        Tags.Clear();
        Tags.AddRange(tags);
    }

    public async Task<Tag> CreateTag()
    {
        var res = ValidationRules.Validate(NewTag);
        if (res.IsValid)
        {
            var created = await _api.CreateAsync(NewTag, default);
            Tags.Add(created);
            NewTag = new();
            return created;
        }

        throw new ArgumentException(res.ToString());
    }

}
