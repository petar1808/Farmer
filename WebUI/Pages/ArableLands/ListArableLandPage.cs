using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.ArableLand;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Pages.ArableLands
{
    public partial class ListArableLandPage
    {
        [Inject]
        public IArableLandService ArableLandService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<ArableLandModel> ArableLands { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            ArableLands = await ArableLandService.List();
        }


        public async Task EditArableLand(int arableLandId)
        {
            await DialogService.OpenAsync<DetailsArableLandPage>($"Земя {arableLandId}",
              new Dictionary<string, object>() { { "ArableLandId", arableLandId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            ArableLands = await ArableLandService.List();
        }

        public async Task AddArableLand()
        {
            await DialogService.OpenAsync<DetailsArableLandPage>($"Земя",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            ArableLands = await ArableLandService.List();
        }

        public async Task DeleteArableLand(int articleId)
        {
            await DialogService.OpenAsync<DetailsArableLandPage>($"Земя",
              options: new DialogOptions() { Width = "700px", Height = "570px" });
            ArableLands = await ArableLandService.List();
        }
    }
}
