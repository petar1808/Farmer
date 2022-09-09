using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.PerformedWork;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Pages.PerformedWork
{
    public partial class DetailsPerformedWorkDialog
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Parameter]
        public int PerformedWorkId { get; set; }

        public PerformedWorkDatailsModel PerformedWork { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;


        public List<SelectionListModel> WorkTypes { get; set; } = new List<SelectionListModel>();

        bool popup;

        protected async override Task OnInitializedAsync()
        {

            WorkTypes = await PerformedWorkService.GetWorkTypes();

            if (PerformedWorkId == 0) //??
            {
                PerformedWork = new PerformedWorkDatailsModel();
            }
            else
            {
                PerformedWork = await PerformedWorkService.Get(PerformedWorkId);
            }
        }

        public void OnDropDownChange(object value)
        {
            PerformedWork.WorkType = (int)value;
        }


        protected async Task OnSubmit(PerformedWorkDatailsModel performedWork/*, int seedingId*/)
        {
            if (performedWork.Id == 0)
            {
                var addIsSuccess = await PerformedWorkService.Add(PerformedWork, 2);

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
                await PerformedWorkService.Update(PerformedWork);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
            DialogService.Close(false);
        }

        void OnDropDownChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
        }

        protected async Task DeleteEmployee()
        {
            await PerformedWorkService.Delete(PerformedWork.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";
        }
    }
}
