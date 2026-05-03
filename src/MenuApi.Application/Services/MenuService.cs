using MenuApi.Application.Abstractions;
using MenuApi.Application.Common;
using MenuApi.Application.Dtos;
using MenuApi.Domain.Entities;


namespace MenuApi.Application.Services;

public class MenuService(IMenuRepository repository, IIdentityService identityService) : IMenuService
{

    public async Task<Result<MenuResponseDto>> GetMenuForUserAsync()
{
    try
    {
        var userRoles = identityService.GetUserRoles();
        var allMenus = await repository.GetAllMenusAsync();
        var rawMenu = allMenus.FirstOrDefault();

        if (rawMenu == null) return Result<MenuResponseDto>.Success(new MenuResponseDto());

        // Här använder vi din FilterMenuItems-metod!
        var filteredSections = rawMenu.MenuSections
            .Where(s => s.Roles.Count == 0 || s.Roles.Intersect(userRoles).Any())
            .Select(s => new MenuSectionDto
            {
                Title = s.Title,
                // ANROP TILL DIN HJÄLPMETOD:
                Items = FilterMenuItems(s.MenuItems, userRoles) 
            })
            .Where(s => s.Items.Any())
            .ToList();

        return Result<MenuResponseDto>.Success(new MenuResponseDto { Sections = filteredSections });
    }
    catch (Exception ex)
    {
        return Result<MenuResponseDto>.Failure($"Kunde inte generera menyn: {ex.Message}");
    }
}

    private List<MenuItemDto> FilterMenuItems(List<MenuItem> items, List<string> userRoles)
    {
        return items
            // 1. Filtrera: Har användaren rätt roll? (Om inga roller krävs, visa för alla)
            .Where(i => i.AllowedRoles.Count == 0 || i.AllowedRoles.Intersect(userRoles).Any())
            // 2. Sortera
            .OrderBy(i => i.SortOrder)
            // 3. Mappa till DTO
            .Select(i => new MenuItemDto
            {
                Title = i.Title,
                Href = i.Href,
                Icon = i.Icon,
                // 4. REKURSION: Kör samma check på alla subitems
                SubItems = FilterMenuItems(i.SubItems, userRoles)
            })
            .ToList();
    }
}
