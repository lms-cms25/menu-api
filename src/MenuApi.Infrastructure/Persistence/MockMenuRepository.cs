using MenuApi.Application.Abstractions;
using MenuApi.Domain.Entities;

namespace MenuApi.Infrastructure.Persistence;

public class MockMenuRepository : IMenuRepository
{
    public Task<List<Menu>> GetAllMenusAsync()
    {
        // 1. Skapa objektet med sub-items
        var coursesWithSubs = new MenuItem
        {
            Title = "Courses",
            Href = "/courses",
            Icon = "CoursesIcon",
            SortOrder = 2,
            AllowedRoles = ["Student", "Instructor"],
            SubItems = [
                new MenuItem { Title = "All Courses", Href = "/courses", SortOrder = 1 },
            new MenuItem { Title = "Add Course", Href = "/courses/add", SortOrder = 2, AllowedRoles = ["Instructor", "Admin"] }
            ]
        };

        // 2. Lägg in det i menyn
        var menus = new List<Menu>
    {
        new Menu
        {
            MenuSections = [
                new MenuSection
                {
                    Title = "MENU",
                    Roles = ["Admin", "Student", "Instructor"],
                    MenuItems = [
                        new MenuItem { Title = "Dashboard", Href = "/dashboard", Icon = "DashboardIcon", SortOrder = 1 },
                        coursesWithSubs, // <--- HÄR LÄGGER VI IN DEN!
                        new MenuItem { Title = "Calendar", Href = "/calendar", Icon = "CalendarIcon", SortOrder = 3 }
                    ]
                }
            ]
        }
    };

        return Task.FromResult(menus);
    }
}
