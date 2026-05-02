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
                        Title = "MENU",
                        Roles = ["Admin", "Student", "Instructor"],
                        MenuItems = new List<MenuItem>
                        {
                            new MenuItem { Title = "Dashboard", Href = "/dashboard", Icon = "DashboardIcon", Roles = ["Admin", "Student", "Instructor"] },
                            new MenuItem { Title = "Courses", Href = "/courses", Icon = "CoursesIcon", Roles = ["Student", "Instructor"] },
                            new MenuItem { Title = "Calendar", Href = "/calendar", Icon = "CalendarIcon", Roles = ["Student", "Instructor"] },
                            new MenuItem { Title = "Live Class", Href = "/liveclass", Icon = "LiveClassIcon", Roles = ["Student", "Instructor"] }
                        }
                    },
                    new MenuSection
                    {
                        Title = "GENERAL",
                        Roles = ["Admin", "Student", "Instructor"],
                        MenuItems = new List<MenuItem>
                        {
                            new MenuItem { Title = "Profile", Href = "/profile", Icon = "ProfileIcon", Roles = ["Admin", "Student", "Instructor"] },
                            new MenuItem { Title = "Team", Href = "/team", Icon = "ProfileIcon", Roles = ["Admin", "Instructor"] }, // Endast Admin/Lärare
                            new MenuItem { Title = "Settings", Href = "/settings", Icon = "SettingsIcon", Roles = ["Admin", "Student", "Instructor"] },
                            new MenuItem { Title = "Help Center", Href = "/helpcenter", Icon = "HelpCenterIcon", Roles = ["Admin", "Student", "Instructor"] },
                            new MenuItem { Title = "Log Out", Href = "#", Icon = "LogoutIcon", Roles = ["Admin", "Student", "Instructor"] }
                        }
                    }
                }
            }
        };

        return Task.FromResult(menus);
    }
}
