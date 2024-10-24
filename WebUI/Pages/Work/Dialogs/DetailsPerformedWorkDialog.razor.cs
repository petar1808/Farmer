using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.PerformedWork;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Pages.Work.Dialogs
{
    public partial class DetailsPerformedWorkDialog
    {
        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public int PerformedWorkId { get; set; }

        public PerformedWorkDatailsModel PerformedWork { get; set; } = default!;

        public List<SelectionListModel> WorkTypes { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            WorkTypes = await PerformedWorkService.GetWorkTypes();

            if (PerformedWorkId == 0)
            {
                PerformedWork = new PerformedWorkDatailsModel();
            }
            else
            {
                PerformedWork = await PerformedWorkService.Get(PerformedWorkId);
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        public void OnDropDownChange(object value)
        {
            PerformedWork.WorkType = (int)value;
        }

        protected async Task OnSubmit(PerformedWorkDatailsModel performedWork)
        {
            bool addIsSuccess = false;

            if (performedWork.Id == 0)
            {
                addIsSuccess = await PerformedWorkService.Add(PerformedWork, SeedingId);
            }
            else
            {
                addIsSuccess = await PerformedWorkService.Update(PerformedWork);
            }
            DialogService.Close(addIsSuccess);
        }
    }
}
