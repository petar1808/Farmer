using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Models;

namespace WebUI.Services
{
    public class NavMenuService
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; } = default!;

        NavMenuItem[] menuItems = new[]
        {
            new NavMenuItem()
            {
                Name = "Номенклатури",
                Icon = "source",
                Children = new List<NavMenuItem>()
                {
                    new NavMenuItem()
                    {
                        Name = "Артикули",
                        Path = "/article/list",
                        Icon = "grass"
                    },
                    new NavMenuItem()
                    {
                        Name = "Земи",
                        Path = "/arableLand/list",
                        Icon = "location_pin"
                    },
                    new NavMenuItem()
                    {
                        Name = "Сезони",
                        Path = "/workingSeason/list",
                        Icon = "calendar_month"
                    }
                }
            },
            new NavMenuItem()
            {
                Name = "Жътва",
                Icon = "agriculture",
                Path = "/seeding"
            },
            new NavMenuItem()
            {
                Name = "Потребители",
                Icon = "group",
                Path = "/listUser"
            }
        };

        public IEnumerable<NavMenuItem> GetMenuItems(string role)
        {
            if (role.ToLower() == "admin")
            {
                return menuItems;
            }  

            return menuItems.Where(x => x.Name != "Потребители");
        }
    }
}
