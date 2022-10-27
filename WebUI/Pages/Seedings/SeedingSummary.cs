using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Extensions;
using WebUI.Services.Article;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Seedings
{
    public partial class SeedingSummary
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public string ArableLandName { get; set; } = default!;

        [Parameter]
        public bool IsModal { get; set; }

        [Parameter]
        public GetSeedingSummaryModel SeedingSummaryData { get; set; } = default!;

        public List<SelectionListModel> AllArticleOfTypeSeeds { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            if (IsModal)
            {
                AllArticleOfTypeSeeds = await ArticleService.GetArticles(ArticleType.Seeds);
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        } 

        public void OnDropDownChange(object value)
        {
            SeedingSummaryData.ArticleId = (int)value;
        }

        public async Task OnEdit()
        {
            await DialogService.OpenAsync<SeedingSummary>($"Редактиране на Сеитба за земя: {ArableLandName}",
                new Dictionary<string, object>() { 
                    { "SeedingId", SeedingId },
                    { "IsModal", true},
                    { "SeedingSummaryData", SeedingSummaryData }
                },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("410px").WithWidth("900px"));

            SeedingSummaryData = await SeedingService.GetSeedingSummary(SeedingId);

            this.StateHasChanged();
        }

        public async Task OnSubmit()
        {
            var response = await SeedingService.UpdateSeedingSummary(SeedingSummaryData, SeedingId);

            DialogService.Close(response);
        }
    }
}
