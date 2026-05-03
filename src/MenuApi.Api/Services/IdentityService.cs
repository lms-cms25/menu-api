using MenuApi.Api.Abstractions;
using System.Security.Claims;

namespace MenuApi.Api.Services;

public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
{
    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;

    public string? UserId => _user?.FindFirstValue(ClaimTypes.NameIdentifier);

    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

    public List<string> GetUserRoles()
    {
        return _user?.FindAll(ClaimTypes.Role)
                     .Select(c => c.Value)
                     .ToList() ?? new List<string>();
    }
}
