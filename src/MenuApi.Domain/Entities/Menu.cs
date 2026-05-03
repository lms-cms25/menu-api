namespace MenuApi.Domain.Entities;

public class Menu
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; // Namn som "Sidebar"
    public List<string> Roles { get; set; } = [];
    public List<MenuSection> MenuSections { get; set; } = [];
}
