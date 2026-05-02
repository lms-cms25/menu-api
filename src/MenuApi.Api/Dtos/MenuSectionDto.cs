namespace MenuApi.Api.Dtos;

public class MenuSectionDto
{
    public string Title { get; set; } = null!;
    public List<MenuItemDto> Items { get; set; } = [];
}
