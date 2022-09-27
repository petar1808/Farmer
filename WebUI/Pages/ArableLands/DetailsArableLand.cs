using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.ArableLand;
using WebUI.ServicesModel.ArableLand;

namespace WebUI.Pages.ArableLands
{
    public partial class DetailsArableLand
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IArableLandService ArableLandService { get; set; } = default!;

        [Parameter]
        public int ArableLandId { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        public ArableLandModel ArableLands { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (ArableLandId == 0)
            {
                IsModal = true;
                ArableLands = new ArableLandModel();
            }
            else
            {
                ArableLands = await ArableLandService.Get(ArableLandId);
            }
        }

        protected async Task OnSubmit(ArableLandModel arableLand)
        {
            if (arableLand.Id == 0)
            {
                var addIsSuccess = await ArableLandService.Add(ArableLands);
                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New ArableLand added successfully.";
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new ArableLand. Please try again.";
                }
            }
            else
            {
                await ArableLandService.Update(ArableLands);
                StatusClass = "alert-success";
                Message = "ArableLand updated successfully.";
            }
            DialogService.Close(false);
        }
    }
}
