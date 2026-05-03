using MenuApi.Application.Abstractions;
using MenuApi.Application.Dtos;
using MenuApi.Application.Common;


namespace MenuApi.Application.Services;

public class MenuService(IMenuRepository repository, IIdentityService identityService) : IMenuService
{

    public async Task<Result<MenuResponseDto>> GetMenuForUserAsync()
    {
        try
        {
            // 1. Hämta rollerna direkt från vår nya tjänst istället för via parameter
            var userRoles = identityService.GetUserRoles();

            // 2. Hämta rådatan från repositoryt
            var allMenus = await repository.GetAllMenusAsync();
            var rawMenu = allMenus.FirstOrDefault();

            if (rawMenu == null)
            {
                return Result<MenuResponseDto>.Success(new MenuResponseDto());
            }

            // 3. Filtrera och mappa baserat på userRoles vi hämtade i steg 1
            var filteredSections = rawMenu.MenuSections
                .Where(s => s.Roles.Intersect(userRoles).Any())
                .Select(s => new MenuSectionDto
                {
                    Title = s.Title,
                    Items = s.MenuItems
                        .Where(i => i.Roles.Intersect(userRoles).Any())
                        .Select(i => new MenuItemDto
                        {
                            Title = i.Title,
                            Href = i.Href,
                            Icon = i.Icon
                        }).ToList()
                })
                .Where(s => s.Items.Any())
                .ToList();

            var response = new MenuResponseDto { Sections = filteredSections };

            return Result<MenuResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            // Om något går snett (t.ex. databasen är nere)
            return Result<MenuResponseDto>.Failure($"Kunde inte generera menyn: {ex.Message}");
        }
    }
}
