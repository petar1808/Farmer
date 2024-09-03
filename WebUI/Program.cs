using Blazored.LocalStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI;
using WebUI.Extensions;
using WebUI.Services;
using WebUI.Services.ArableLand;
using WebUI.Services.Article;
using WebUI.Services.Identity;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.Services.Subsidies;
using WebUI.Services.Tenants;
using WebUI.Services.Treatment;
using WebUI.Services.WorkingSeasons;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddRadzenComponents();

builder.Services
    .AddBlazorise(options =>
    {
        options.Immediate = true;
    })
    .AddBootstrapProviders()
    .AddFontAwesomeIcons();

builder.Services.AddSingleton<NavMenuService>();

builder.Services.AddSingleton<SelectedWorkingSeasonService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiURL")!)
    });


builder.Services.AddScoped<IHttpService, HttpService>();

builder.Services.AddTransient<IArticleService, ArticleService>();
builder.Services.AddTransient<IArableLandService, ArableLandService>();
builder.Services.AddTransient<IWorkingSeasonService, WorkingSeasonService>();
builder.Services.AddTransient<IPerformedWorkService, PerformedWorkService>();
builder.Services.AddTransient<ITreatmentService, TreatmentService>();
builder.Services.AddTransient<ISeedingService, SeedingService>();
builder.Services.AddTransient<IIdentityService, IdentityService>();
builder.Services.AddTransient<ISubsidyService, SubsidyService>();
builder.Services.AddTransient<ITenantService, TenantService>();

builder.Services.AddFluxor(conf =>
{
    conf
    .ScanAssemblies(typeof(Program).Assembly)
    .UseReduxDevTools();
});


await builder.Build().RunAsync();
