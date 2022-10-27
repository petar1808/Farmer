using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.WorkingSeasons
{
    public partial class DetailsWorkingSeason
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int WorkingSeasonId { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        public WorkingSeasonModel WorkingSeason { get; set; } = default!;

        protected async override Task OnInitializedAsync()
        {
            if (WorkingSeasonId == 0)
            {
                IsModal = true;
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
            // endDate.year - StartDate.year = 1
            // 

            ChangeName(WorkingSeason.StartDate, endDate);
        }

        protected async Task OnSubmit(WorkingSeasonModel workingSeason)
        {
            if (workingSeason.Id == 0)
            {
                var addIsSuccess = await WorkingSeasonService.Add(WorkingSeason);
                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New WorkingSeason added successfully.";
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new WorkingSeason. Please try again.";
                }
            }
            else
            {
                await WorkingSeasonService.Update(WorkingSeason);
                StatusClass = "alert-success";
                Message = "WorkingSeason updated successfully.";
            }
            DialogService.Close(false);
        }
    }
}
