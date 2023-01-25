using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.ArableLand;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Pages.ArableLands
{
    public partial class DetailsArableLand
    {
        [Inject]
        public IArableLandService ArableLandService { get; set; } = default!;

        [Parameter]
        public int ArableLandId { get; set; }

        public ArableLandModel ArableLands { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (ArableLandId == 0)
            {
                ArableLands = new ArableLandModel();
            }
            else
            {
                ArableLands = await ArableLandService.Get(ArableLandId);
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(ArableLandModel arableLand)
        {
            bool addIsSuccess = false;

            if (arableLand.Id == 0)
            {
                addIsSuccess = await ArableLandService.Add(ArableLands);
            }
            else
            {
                addIsSuccess = await ArableLandService.Update(ArableLands);
            }

            DialogService.Close(addIsSuccess);
        }
    }
}
