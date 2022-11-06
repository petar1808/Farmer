using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Extensions;
using WebUI.Services.Article;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;
using WebUI.ServicesModel.Seeding;
using WebUI.Store;

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
        public int SizeInDecar { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        public GetSeedingSummaryModel SeedingSummaryData { get; set; } = default!;

        public List<SelectionListModel> AllArticleOfTypeSeeds { get; set; } = new List<SelectionListModel>();

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (IsModal)
            {
                AllArticleOfTypeSeeds = await ArticleService.GetArticles(ArticleType.Seeds);
            }

            SeedingSummaryData = await SeedingService.GetSeedingSummary(SeedingId);
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
            var dialogResult = await DialogService.OpenAsync<SeedingSummary>($"Редактиране на Сеитба за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { 
                    { "SeedingId", SeedingId },
                    { "IsModal", true}
                },
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("500px").WithWidth("900px"));

            if (dialogResult != null)
            {
                SeedingSummaryData = await SeedingService.GetSeedingSummary(SeedingId);
                await UpdateArableLandBalance(SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task OnSubmit()
        {
            var response = await SeedingService.UpdateSeedingSummary(SeedingSummaryData, SeedingId);

            DialogService.Close(response);
        }

        private async Task UpdateArableLandBalance(int seedingId)
        {
            this.Dispatcher.Dispatch(
                new UpdateSeedingArableLandBalance(await SeedingService.GetArableLandBalance(seedingId))
                );
        }
    }
}
