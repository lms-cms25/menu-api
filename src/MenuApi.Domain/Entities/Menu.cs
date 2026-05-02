namespace MenuApi.Domain.Entities;

public class Menu
{
    public List<MenuSection> MenuSections { get; set; } = [];
    public List<string> Roles { get; set; } = [];
}
