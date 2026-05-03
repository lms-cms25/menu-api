using MenuApi.Application.Dtos;
using MenuApi.Application.Common;

namespace MenuApi.Application.Abstractions;

public interface IMenuService
{
    Task<Result<MenuResponseDto>> GetMenuForUserAsync();
}
