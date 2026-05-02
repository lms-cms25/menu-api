using MenuApi.Application.Abstractions;
using MenuApi.Domain.Entities;

namespace MenuApi.Application;
public class MockMenuRepository : IMenuRepository
{
    public Task<List<Menu>> GetAllMenusAsync()
    {
        var menus = new List<Menu>
        {
            new Menu
            {
                Roles = ["Admin", "Student", "Instructor"],
                MenuSections = new List<MenuSection>
                {
                    new MenuSection
                    {
                        Title = "Utbildning",
                        Roles = ["Student", "Instructor"],
                        MenuItems = new List<MenuItem>
                        {
                            new MenuItem { Title = "Mina Kurser", Href = "/courses", Roles = ["Student", "Instructor"] },
                            new MenuItem { Title = "Betygsättning", Href = "/grading", Roles = ["Instructor"] }
                        }
                    },
                    new MenuSection
                    {
                        Title = "Administration",
                        Roles = ["Admin"],
                        MenuItems = new List<MenuItem>
                        {
                            new MenuItem { Title = "Hantera Användare", Href = "/users", Roles = ["Admin"] },
                            new MenuItem { Title = "Systemloggar", Href = "/logs", Roles = ["Admin"] }
                        }
                    }
                }
            }
        };

        return Task.FromResult(menus);
    }
}
