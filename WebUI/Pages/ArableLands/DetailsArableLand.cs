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

        public ArableLandModel ArableLands { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        bool popup;

        protected async override Task OnInitializedAsync()
        {
            if (ArableLandId == 0) //new employee is being created
            {
                //add some defaults
                ArableLands = new ArableLandModel();
            }
            else
            {
                ArableLands = await ArableLandService.Get(ArableLandId);
            }
        }


        protected async Task OnSubmit(ArableLandModel arableLand)
        {
            if (arableLand.Id == 0) //new
            {
                var addIsSuccess = await ArableLandService.Add(ArableLands);
                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";

                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                }
            }
            else
            {
                await ArableLandService.Update(ArableLands);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
            DialogService.Close(false);
        }

        protected async Task DeleteEmployee()
        {
            await ArableLandService.Delete(ArableLands.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";
        }
    }
}
