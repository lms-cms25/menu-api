using MenuApi.Domain.Entities;

namespace MenuApi.Application.Abstractions;
public interface IMenuRepository
{
    Task<List<Menu>> GetAllMenusAsync();
}
