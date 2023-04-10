namespace Fit.Demo.Domain;

public record class Tag
{
    public int Id { get; set; }
    public int ToDoId { get; set; }
    public string? Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool Done { get; set; } = false;
}
