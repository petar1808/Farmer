using WebUI;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WebUI.Services;
using Radzen;
using WebUI.Extensions;
using WebUI.Services.ArableLand;
using WebUI.Services.WorkingSeasons;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddRadzenComponents();

builder.Services.AddSingleton<NavMenuService>();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//var apiUrl = "https://localhost:5001/";

var apiUrl = "https://farmerapi-dev.azurewebsites.net/";

builder.Services.AddHttpClient<IArticleService, ArticleService>(client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IArableLandService, ArableLandService>(client => client.BaseAddress = new Uri(apiUrl));
builder.Services.AddHttpClient<IWorkingSeasonService, WorkingSeasonService>(client => client.BaseAddress = new Uri(apiUrl));

await builder.Build().RunAsync();
