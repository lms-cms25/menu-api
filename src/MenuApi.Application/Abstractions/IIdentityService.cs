namespace MenuApi.Application.Abstractions;

public interface IIdentityService
{
    string? UserId { get; }
    List<string> GetUserRoles();
    bool IsAuthenticated { get; }
}
