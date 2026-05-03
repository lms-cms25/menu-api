using MenuApi.Application.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace MenuApi.Infrastructure.Services;

public class IdentityService(IHttpContextAccessor httpContextAccessor) : IIdentityService
{


    //public string? UserId => _user?.FindFirstValue(ClaimTypes.NameIdentifier);

    private readonly ClaimsPrincipal? _user = httpContextAccessor.HttpContext?.User;
    public string? UserId => _user?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;


    public bool IsAuthenticated => _user?.Identity?.IsAuthenticated ?? false;

    public List<string> GetUserRoles()
    {
        return _user?.FindAll(ClaimTypes.Role)
                     .Select(c => c.Value)
                     .ToList() ?? new List<string>();
    }
}
