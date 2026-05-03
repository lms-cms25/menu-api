namespace MenuApi.Application.Dtos;

public class MenuItemDto
{
    public string Title { get; set; } = null!;
    public string Href { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public List<MenuItemDto> SubItems { get; set; } = [];
}