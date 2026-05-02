namespace MenuApi.Domain.Entities;

public class MenuSection
{
    public string Title { get; set; } = null!;
    public List<MenuItem> MenuItems { get; set; } = [];
    public List<string> Roles { get; set; } = [];
}
