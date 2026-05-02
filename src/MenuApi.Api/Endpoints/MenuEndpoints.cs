using MenuApi.Api.Abstractions;

namespace MenuApi.Api.Endpoints;

public static class MenuEndpoints
{
    public static void MapMenuEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/menu").WithTags("Menu").WithDescription("Side bar menu");

        group.MapGet("/get", GetMenu);

    }
    public static async Task<IResult> GetMenu(IMenuService menuService)
    {
        // Här skulle du i framtiden hämta roller från User.Claims
        // Just nu hårdkodar vi för att du ska kunna testa olika scenarion:
        var mockUserRoles = new List<string> { "Student", "Instructor" };

        // 1. Anropa din service
        var result = await menuService.GetMenuForUserAsync(mockUserRoles);

        // 2. Hantera resultatet baserat på ditt Result-pattern
        if (!result.IsSuccess)
        {
            return Results.BadRequest(result.Error);
        }

        // 3. Returnera datan (Value är din MenuResponseDto)
        return Results.Ok(result.Value);
    }

    
}
