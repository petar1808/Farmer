using WebUI.Models;

namespace WebUI.Services
{
    public class NavMenuService
    {
        NavMenuItem[] menuItems = new[]
        {
            new NavMenuItem()
            {
                Name = "Номенклатури",
                Icon = "&#xe871",
                Title = "How to get started with the Radzen Blazor components",
                Children = new List<NavMenuItem>()
                {
                    new NavMenuItem()
                    {
                        Name = "Артикули",
                        Path = "/article/list",
                        Icon = "&#xe871",
                        Title = "How to get started with the Radzen Blazor components"
                    },
                    new NavMenuItem()
                    {
                        Name = "Земя",
                        Path = "/arableLand/list",
                        Icon = "&#xe871",
                        Title = "How to get started with the Radzen Blazor components"
                    },
                    new NavMenuItem()
                    {
                        Name = "Сезон",
                        Path = "/workingSeason/list",
                        Icon = "&#xe871",
                        Title = "How to get started with the Radzen Blazor components"
                    }
                }
            },
            new NavMenuItem()
            {
                Name = "Жътва",
                Icon = "&#xe664",
                Path = "/seeding"
            }
        };

        public IEnumerable<NavMenuItem> MenuItems => menuItems;
    }
}
