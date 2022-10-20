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

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int TreatmentId { get; set; }

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public bool IsModal { get; set; }

        public ТreatmentDetailsModel Treatment { get; set; } = default!;

        public List<SelectionListModel> TreatmentTypes { get; set; } = new List<SelectionListModel>();

        public List<SelectionListModel> TreatmentArticleType { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        { 
            TreatmentTypes = await TreatmentService.GetTreatmentTypes();
            TreatmentArticleType = await TreatmentService.GetTreatmentArticles();

            if (TreatmentId == 0) 
            {
                IsModal = true;
                Treatment = new ТreatmentDetailsModel();
            }
            else
            {
                Treatment = await TreatmentService.Get(TreatmentId);
            }
        }

        public void OnDropDownChangeTreatmentType(object value)
        {
            Treatment.TreatmentType = (int)value;
        }

        public void OnDropDownChangeTreatmentArticleType(object value)
        {
            Treatment.ArticleId = (int)value;
        }

        protected async Task OnSubmit(ТreatmentDetailsModel treatment)
        {
            if (treatment.Id == 0)
            {
                var addIsSuccess = await TreatmentService.Add(treatment, SeedingId);

                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New Treatment added successfully.";
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new Treatment. Please try again.";
                }
            }
            else
            {
                await TreatmentService.Update(Treatment);
                StatusClass = "alert-success";
                Message = "Treatment updated successfully.";
            }
            DialogService.Close(false);
        }
    }
}
