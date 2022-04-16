using Application.Extensions;
using Application.Services;
using Application.Services.WorikingSeasons;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers;
using Web.ViewModels.Components;

namespace Web.Components
{

    [ViewComponent(Name = "SidebarMenuComponent")]
    public class SidebarMenuComponent : ViewComponent
    {
        private const string menuOpenAttribute = "menu-open";
        private const string subMenuActiveAttribute = "active";

        private readonly IWorkingSeasonService workingSeasonService;
        private readonly SidebarMenuCache sidebarMenuCache;
        public SidebarMenuComponent(
            IWorkingSeasonService workingSeasonService,
            SidebarMenuCache sidebarMenuCache)
        {
            this.workingSeasonService = workingSeasonService;
            this.sidebarMenuCache = sidebarMenuCache;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contoller = RouteData.Values["controller"]!.ToString();
            var routeKey = RouteData.Values["id"];

            var result = new List<SidebarMenuViewModel>()
            {
                new SidebarMenuViewModel
                {
                    Title = "Номенклатури",
                    MenuOpen = GetNomenclaturesMenuOpenValue(contoller!),
                    Childs = new List<SidebarChildElentViewModel>()
                    {
                        new SidebarChildElentViewModel()
                        {
                            Title = "Сезони",
                            ControllerName = nameof(WorkingSeasonsController).RemoveControllerFromName(),
                            ActionName = nameof(WorkingSeasonsController.All),
                            Active = contoller == nameof(WorkingSeasonsController).RemoveControllerFromName() ? subMenuActiveAttribute : ""
                        },
                        new SidebarChildElentViewModel()
                        {
                            Title = "Земи",
                            ControllerName = nameof(ArableLandsController).RemoveControllerFromName(),
                            ActionName = nameof(ArableLandsController.All),
                            Active = contoller == nameof(ArableLandsController).RemoveControllerFromName() ? subMenuActiveAttribute : ""
                        },
                        new SidebarChildElentViewModel()
                        {
                            Title = "Артикули",
                            ControllerName = nameof(ArticlesController).RemoveControllerFromName(),
                            ActionName = nameof(ArticlesController.All),
                            Active = contoller == nameof(ArticlesController).RemoveControllerFromName() ? subMenuActiveAttribute : ""
                        }
                    }
                }
            };

            var seasons = new Dictionary<int, string>();

            if (sidebarMenuCache.SidebarMenuItems.Any())
            {
                seasons = sidebarMenuCache.SidebarMenuItems;
            }
            else
            {
                seasons = await this.workingSeasonService.ListSidebarMenuItems();
                sidebarMenuCache.AddSidebarMenuItems(seasons);
            }

            // await this.workingSeasonService.ListSidebarMenuItems();

            var seasonsSidebarElements = seasons.Select(x => new SidebarChildElentViewModel
            {
                Title = x.Value,
                ControllerName = nameof(SeedingsController).RemoveControllerFromName(),
                ActionName = nameof(SeedingsController.List),
                Raute = x.Key,
                Active = GetWorkingSeasonChildActiveValue(routeKey, x.Key)
            }).ToList();

            result.Add(new SidebarMenuViewModel
            {
                Title = "Сеидба",
                MenuOpen = contoller == nameof(SeedingsController).RemoveControllerFromName() ? menuOpenAttribute : "",
                Childs = seasonsSidebarElements
            });

            return View(result);
        }


        private string GetNomenclaturesMenuOpenValue(string controllerName)
        {
            if (controllerName == nameof(WorkingSeasonsController).RemoveControllerFromName()
                || controllerName == nameof(ArableLandsController).RemoveControllerFromName()
                || controllerName == nameof(ArticlesController).RemoveControllerFromName())
            {
                return menuOpenAttribute;
            }
            else
            {
                return "";
            }
        }

        private string GetWorkingSeasonChildActiveValue(object? routeKey, int seasonId)
        {
            if (routeKey == null)
            {
                return "";
            }

            if (int.TryParse(routeKey.ToString(), out int value))
            {
                return seasonId == value ? subMenuActiveAttribute : "";
            }

            return "";
        }
    }
}
