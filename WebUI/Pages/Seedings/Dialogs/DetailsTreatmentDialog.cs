using Blazorise;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Article;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;
using WebUI.ServicesModel.Тreatment;

namespace WebUI.Pages.Seedings.Dialogs
{
    public partial class DetailsTreatmentDialog
    {
        private const string sprayingDisplayName = "Препарат";
        private const string fertilizationDisplayName = "Тор";
        private string treatmentTypePlaceholder = "";

        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int TreatmentId { get; set; }

        [Parameter]
        public int SeedingId { get; set; }

        public ТreatmentDetailsModel Treatment { get; set; } = default!;

        public List<SelectionListModel> TreatmentTypes { get; set; } = new List<SelectionListModel>();

        public List<SelectionListModel> Articles { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            TreatmentTypes = await TreatmentService.GetTreatmentTypes();

            if (TreatmentId == 0)
            {
                Treatment = new ТreatmentDetailsModel();
            }
            else
            {
                Treatment = await TreatmentService.Get(TreatmentId);
                treatmentTypePlaceholder = Treatment.TreatmentType == (int)ТreatmentType.Spraying ? sprayingDisplayName : fertilizationDisplayName;
                if ((ТreatmentType)Treatment.TreatmentType == ТreatmentType.Spraying)
                {
                    Articles = await ArticleService.GetArticles(ArticleType.Preparations);
                }
                if ((ТreatmentType)Treatment.TreatmentType == ТreatmentType.Fertilization)
                {
                    Articles = await ArticleService.GetArticles(ArticleType.Fertilizers);
                }

            }
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        public async Task OnDropDownChangeTreatmentType(object value)
        {
            Treatment.TreatmentType = (int)value;

            if ((ТreatmentType)value == ТreatmentType.Spraying)
            {
                treatmentTypePlaceholder = sprayingDisplayName;
                Articles = await ArticleService.GetArticles(ArticleType.Preparations);
            }
            if ((ТreatmentType)value == ТreatmentType.Fertilization)
            {
                treatmentTypePlaceholder = fertilizationDisplayName;
                Articles = await ArticleService.GetArticles(ArticleType.Fertilizers);
            }
        }

        public void OnDropDownChangeTreatmentArticleType(object value)
        {
            Treatment.ArticleId = (int)value;
        }

        protected async Task OnSubmit(ТreatmentDetailsModel treatment)
        {
            bool addIsSuccess = false;

            if (treatment.Id == 0)
            {
                addIsSuccess = await TreatmentService.Add(treatment, SeedingId);
            }
            else
            {
                addIsSuccess = await TreatmentService.Update(Treatment);
            }
            DialogService.Close(addIsSuccess);
        }
    }
}
