namespace Fit.Demo.Domain;

public record class ToDo
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; } = string.Empty;
}
