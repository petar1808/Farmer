using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.WorkingSeasons;
using WebUI.ServicesModel.WorkingSeason;

namespace WebUI.Pages.WorkingSeasons
{
    public partial class DetailsWorkingSeasonPage
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IWorkingSeasonService WorkingSeasonService { get; set; } = default!;

        [Parameter]
        public int WorkingSeasonId { get; set; }

        public WorkingSeasonModel WorkingSeason { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        bool popup;

        protected async override Task OnInitializedAsync()
        {
            if (WorkingSeasonId == 0) //new employee is being created
            {
                //add some defaults
                WorkingSeason = new WorkingSeasonModel();
            }
            else
            {
                WorkingSeason = await WorkingSeasonService.Get(WorkingSeasonId);
            }
        }


        private void ChangeName(DateTime? startDate, DateTime? endDate)
        {
            string start = startDate == null ? "" : startDate.Value.ToString("yyyy");
            string end = endDate == null ? "" : endDate.Value.ToString("yyyy");
            WorkingSeason.Name = $"{start}/{end}";
        }


        protected void OnDateFromChange(DateTime? value)
        {
            ChangeName(value, WorkingSeason.EndDate);
        }

        protected void OnDateToChange(DateTime? value)
        {
            ChangeName(WorkingSeason.StartDate, value);
        }

        protected async Task OnSubmit(WorkingSeasonModel arableLand)
        {
            if (arableLand.Id == 0) //new
            {
                var addIsSuccess = await WorkingSeasonService.Add(WorkingSeason);
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
                await WorkingSeasonService.Update(WorkingSeason);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
            DialogService.Close(false);
        }

        protected async Task DeleteEmployee()
        {
            await WorkingSeasonService.Delete(WorkingSeason.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";
        }
    }
}
