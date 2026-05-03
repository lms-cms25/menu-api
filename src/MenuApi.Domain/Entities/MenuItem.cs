namespace MenuApi.Domain.Entities;

public class MenuItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Href { get; set; } = string.Empty;
    public string Icon { get; set; } = string.Empty;
    public int SortOrder { get; set; }

    // För submenyer (Self-referencing)
    public Guid? ParentMenuItemId { get; set; }
    public List<MenuItem> SubItems { get; set; } = [];

    // Behörighet
    public List<string> AllowedRoles { get; set; } = [];
}
