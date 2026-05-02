namespace MenuApi.Domain.Entities;

public class MenuItem
{
    public string Title { get; set; } = null!;
    public string Href { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public List<string> Roles = [];
}
