using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using WebUI.Models;
using static WebUI.IdentityConstants;

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
                    }
                }
            },
            new NavMenuItem()
            {
                Name = "Жътва",
                Icon = "agriculture",
                Path = "/workingSeason"
            },
            new NavMenuItem()
            {
                Name = "Потребители",
                Icon = "group",
                Path = "/listUser"
            },
            new NavMenuItem()
            {
                Name = "Организации и потребители",
                Icon = "group",
                Path = "/tenantsWithUsers"
            }
        };

        public IEnumerable<NavMenuItem> GetMenuItems(string role)
        {
            if (role.ToLower() == IdentityRoles.AdminRole.ToLower())
            {
                return menuItems.Where(x => x.Name != "Организации и потребители");
            }
            if (role.ToLower() == IdentityRoles.SystemAdminRole.ToLower())
            {
                return menuItems.Where(x => x.Name == "Организации и потребители");
            }

            return menuItems.Where(x => x.Name != "Потребители" && x.Name != "Организации и потребители");
        }
    }
}
