using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Pages.Seedings
{
    public partial class DetailsTreatmentDialog
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Parameter]
        public int TreatmentId { get; set; }

        [Parameter]
        public int SeedingId { get; set; }

        public ТreatmentDetailsModel Тreatment { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;


        public List<SelectionListModel> TreatmentTypes { get; set; } = new List<SelectionListModel>();

        bool popup;

        protected async override Task OnInitializedAsync()
        {

            TreatmentTypes = await TreatmentService.GetTreatmentTypes();

            if (TreatmentId == 0) 
            {
                Тreatment = new ТreatmentDetailsModel();
            }
            else
            {
                Тreatment = await TreatmentService.Get(TreatmentId);
            }
        }

        public void OnDropDownChange(object value)
        {
            Тreatment.ТreatmentType = (int)value;
        }


        protected async Task OnSubmit(ТreatmentDetailsModel тreatment)
        {
            if (тreatment.Id == 0)
            {
                var addIsSuccess = await TreatmentService.Add(тreatment, SeedingId);

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
                await TreatmentService.Update(Тreatment);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
            DialogService.Close(false);
        }
    }
}
