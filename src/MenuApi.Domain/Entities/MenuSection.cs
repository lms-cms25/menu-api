namespace MenuApi.Domain.Entities;

public class MenuSection
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int SortOrder { get; set; }


    public List<string> Roles { get; set; } = [];
    public List<MenuItem> MenuItems { get; set; } = [];
}
