using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Subsidies;

namespace WebUI.Pages.Seedings.Dialogs
{
    public partial class DetailsSubsidyDialog
    {
        [Inject]
        public ISubsidyService SubsidyService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public int SubsidyId { get; set; }

        public SubsidiesModel Subsidies { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (SubsidyId == 0)
            {
                Subsidies = new SubsidiesModel();
            }
            else
            {
                Subsidies = await SubsidyService.Get(SubsidyId);
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(SubsidiesModel subsidiesModel)
        {
            bool addIsSuccess = false;

            if (subsidiesModel.Id == 0)
            {
                addIsSuccess = await SubsidyService.Add(Subsidies, SeedingId);
            }
            else
            {
                addIsSuccess = await SubsidyService.Update(Subsidies);
            }
            DialogService.Close(addIsSuccess);
        }
    }
}
