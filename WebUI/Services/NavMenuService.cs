﻿using Microsoft.AspNetCore.Components;
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
                        Name = "Земи",
                        Path = "/arableLand/list",
                        Icon = "&#xe871",
                        Title = "How to get started with the Radzen Blazor components"
                    },
                    new NavMenuItem()
                    {
                        Name = "Сезони",
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
            },
            new NavMenuItem()
            {
                Name = "Потребители",
                Icon = "&#xe664",
                Path = "/listUser"
            }
        };

        public IEnumerable<NavMenuItem> GetMenuItems(string role)
        {
            if (role.ToLower() == "admin")
            {
                return menuItems;
            }  
            //To do: if isAdmin add the admin menu
            return menuItems.Where(x => x.Name != "Потребители");
        }
    }
}
