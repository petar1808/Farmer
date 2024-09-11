using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.WorkingSeasons
{
    public partial class WorkingSeasonDialog
    {
        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int WorkingSeasonId { get; set; }

        public WorkingSeasonModel WorkingSeason { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (WorkingSeasonId == 0)
            {
                WorkingSeason = new WorkingSeasonModel();
            }
            else
            {
                WorkingSeason = await WorkingSeasonService.Get(WorkingSeasonId);
            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        private void ChangeName(DateTime? startDate, DateTime? endDate)
        {
            string start = startDate == null ? "" : startDate.Value.ToString("yyyy");
            string end = endDate == null ? "" : endDate.Value.ToString("yyyy");
            WorkingSeason.Name = $"{start}/{end}";
        }

        protected void OnStartDateChange(DateTime? stratDate)
        {
            ChangeName(stratDate, WorkingSeason.EndDate);
        }

        protected void OnEndDateChange(DateTime? endDate)
        {
            ChangeName(WorkingSeason.StartDate, endDate);
        }

        protected async Task OnSubmit(WorkingSeasonModel workingSeason)
        {
            bool addIsSuccess = false;

            if (workingSeason.Id == 0)
            {
                addIsSuccess = await WorkingSeasonService.Add(WorkingSeason);
            }
            else
            {
                addIsSuccess = await WorkingSeasonService.Update(WorkingSeason);
            }
            DialogService.Close(addIsSuccess);
        }
    }
}
