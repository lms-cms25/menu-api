using MenuApi.Api.Dtos;
using MenuApi.Application.Common;

namespace MenuApi.Api.Abstractions;

public interface IMenuService
{
    Task<Result<MenuResponseDto>> GetMenuForUserAsync(List<string> roles);
}
