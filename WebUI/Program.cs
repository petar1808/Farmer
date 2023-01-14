using WebUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI.Services;
using WebUI.Extensions;
using WebUI.Services.ArableLand;
using WebUI.Services.WorkingSeasons;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using WebUI.Services.PerformedWork;
using WebUI.Services.Treatment;
using WebUI.Services.Seeding;
using WebUI.Services.Article;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.LocalStorage;
using WebUI.Services.Identity;
using Fluxor;
using WebUI.Services.Subsidies;
using WebUI.Services.Tenants;

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

string apiUrl;

if (builder.HostEnvironment.Environment == "Development")
{
    apiUrl = "https://localhost:5001/";
}
else
{
    apiUrl = "https://farmer-api-production.azurewebsites.net/";

}

builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(apiUrl)
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
