using Application.Extensions;
using Application.Services;
using Application.Services.WorikingSeasons;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Web.Areas.Identity.Controllers;
using Web.Controllers;
using Web.ControllersOld;
using Web.ViewModels.Components;
using static Infrastructure.IdentityConstants;
using static Web.WebConstraints.Areas;

namespace Web.Components
{

    [ViewComponent(Name = "SidebarMenuComponent")]
    public class SidebarMenuComponent : ViewComponent
    {
        private const string menuOpenAttribute = "menu-open";
        private const string subMenuActiveAttribute = "active";

        private readonly IWorkingSeasonService workingSeasonService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly SidebarMenuCache sidebarMenuCache;
        public SidebarMenuComponent(
            IWorkingSeasonService workingSeasonService,
            SidebarMenuCache sidebarMenuCache, 
            IHttpContextAccessor httpContextAccessor)
        {
            this.workingSeasonService = workingSeasonService;
            this.sidebarMenuCache = sidebarMenuCache;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var contoller = RouteData.Values["controller"]!.ToString();
            var routeKey = RouteData.Values["id"];

            var userRole = this.httpContextAccessor!
                .HttpContext!
                .User
                .Claims
                .FirstOrDefault(x => x.Type == ClaimTypes.Role);

            if (userRole == null)
            {
                return View(new List<SidebarMenuViewModel>());
            }

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

             await this.workingSeasonService.ListSidebarMenuItems();

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

            if (userRole.Value == IdentityRoles.AdminRole)
            {
                result.Add(new SidebarMenuViewModel
                {
                    Title = "Администрация",
                    Childs = new List<SidebarChildElentViewModel>()
                    {
                        new SidebarChildElentViewModel()
                        {
                            Title = "Потребители",
                            ControllerName = nameof(IdentityController).RemoveControllerFromName(),
                            ActionName = nameof(IdentityController.ListUsers),
                            Active = contoller == nameof(IdentityController).RemoveControllerFromName() ? subMenuActiveAttribute : "",
                            Area = Identity
                        }
                    }
                }) ;
            }

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
