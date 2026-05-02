using MenuApi.Api.Abstractions;
using MenuApi.Api.Dtos;
using MenuApi.Application.Abstractions;
using MenuApi.Application.Common;

namespace MenuApi.Api.Services;

public class MenuService(IMenuRepository repository) : IMenuService
{

    public async Task<Result<MenuResponseDto>> GetMenuForUserAsync(List<string> userRoles)
    {
        try
        {
            // 1. Hämta rådatan
            var allMenus = await repository.GetAllMenusAsync();

            var rawMenu = allMenus.FirstOrDefault();

            // Om ingen meny finns, är det ett fel eller bara en tom framgång?
            // Här kör vi på en "Success" men med tom data.
            if (rawMenu == null)
            {
                return Result<MenuResponseDto>.Success(new MenuResponseDto());
            }

            // 2. Filtrera och mappa
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

            // 3. Returnera med Success-wrapper
            return Result<MenuResponseDto>.Success(response);
        }
        catch (Exception ex)
        {
            // Om något går snett (t.ex. databasen är nere)
            return Result<MenuResponseDto>.Failure($"Kunde inte generera menyn: {ex.Message}");
        }
    }
}
