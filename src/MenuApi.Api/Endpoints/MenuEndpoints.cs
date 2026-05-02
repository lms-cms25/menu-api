namespace MenuApi.Api.Endpoints;

public static class MenuEndpoints
{
    public static void MapMenuEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/menu").WithTags("Menu").WithDescription("Side bar menu");


    }
}
