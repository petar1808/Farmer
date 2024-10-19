﻿using Microsoft.AspNetCore.Components;
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
                Icon = "category",
                Children = new List<NavMenuItem>()
                {
                    new NavMenuItem()
                    {
                        Name = "Семена",
                        Path = "nomenclature/article/1",
                        Icon = "spa"
                    },
                    new NavMenuItem()
                    {
                        Name = "Торове",
                        Path = "nomenclature/article/2",
                        Icon = "Compost"
                    },
                    new NavMenuItem()
                    {
                        Name = "Препарати",
                        Path = "nomenclature/article/3",
                        Icon = "science"
                    },
                    new NavMenuItem()
                    {
                        Name = "Земи",
                        Path = "nomenclature/arableLand",
                        Icon = "terrain"
                    },
                    new NavMenuItem()
                    {
                        Name = "Сезони",
                        Path = "nomenclature/farmingSeason",
                        Icon = "event"
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
                Name = "Субсидии",
                Icon = "monetization_on",
                Path = "/subsidy"
            },
            new NavMenuItem()
            {
                Name = "Разходи",
                Icon = "receipt",
                Path = "/expense"
            },
            new NavMenuItem()
            {
                Name = "Потребители",
                Icon = "people",
                Path = "/listUser"
            },
            new NavMenuItem()
            {
                Name = "Организации и потребители",
                Icon = "business",
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
