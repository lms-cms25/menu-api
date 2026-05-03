using MenuApi.Api.Abstractions;

namespace MenuApi.Api.Endpoints;

public static class MenuEndpoints
{
    public static void MapMenuEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/menu").WithTags("Menu").WithDescription("Side bar menu").RequireAuthorization();

        group.MapGet("/get", GetMenu);

    }
    public static async Task<IResult> GetMenu(IMenuService menuService)
    {
        var result = await menuService.GetMenuForUserAsync();

        return result.IsSuccess
            ? Results.Ok(result.Value)
            : Results.BadRequest(result.Error);
    }


}
