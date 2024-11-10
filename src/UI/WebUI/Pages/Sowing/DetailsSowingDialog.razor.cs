using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Article;
using WebUI.Services.Seeding;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;
using WebUI.ServicesModel.Seeding;

namespace WebUI.Pages.Sowing
{
    public partial class DetailsSowingDialog
    {
        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        public DetailsSowingModel DetailsSowingData { get; set; } = default!;

        public List<SelectionListModel> AllArticleOfTypeSeeds { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            AllArticleOfTypeSeeds = await ArticleService.GetArticles(ArticleType.Seeds);

            DetailsSowingData = await SeedingService.GetSowingDetails(SeedingId);
        }

        public void OnDropDownChange(object value)
        {
            DetailsSowingData.ArticleId = (int)value;
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }
        protected async Task OnSubmit(DetailsSowingModel data)
        {
            bool isSuccess = await SeedingService.UpdateSeedingSummaryNew(data, SeedingId);

            DialogService.Close(isSuccess);
        }

    }
}
