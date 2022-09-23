using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings
{
    public partial class SeedingSummary
    {
        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public GetSeedingSummaryModel GetSeedingModel { get; set; } = default!;

        public List<SelectionListModel> AllArticleOfTypeSeeds { get; set; } = new List<SelectionListModel>();


        protected async override Task OnParametersSetAsync()
        {
            GetSeedingModel = await SeedingService.GetSeedingSummary(SeedingId);

            if (IsModal)
            {
                AllArticleOfTypeSeeds = await ArticleService.GetSeeds();
            }
        }
        public void OnDropDownChange(object value)
        {
            GetSeedingModel.ArticleId = (int)value;
        }


        public async Task OnEdit()
        {
            await DialogService.OpenAsync<SeedingSummary>($"Сеитба",
                new Dictionary<string, object>() { 
                    { "SeedingId", SeedingId },
                    { "IsModal", true}
                },
                options: new DialogOptions() { Width = "700px", Height = "570px" });

            GetSeedingModel = await SeedingService.GetSeedingSummary(SeedingId);

            this.StateHasChanged();
        }

        public async Task OnSubmit()
        {
            var response = await SeedingService.UpdateSeedingSummary(GetSeedingModel, SeedingId);

            DialogService.Close(response);
        }
    }
}
