using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Pages.Work.Dialogs;
using WebUI.Services.PerformedWork;
using WebUI.Services.Seeding;
using WebUI.Services.Treatment;
using WebUI.ServicesModel.Тreatment;
using WebUI.Store;
using WebUI.Store.WorkingSeason;

namespace WebUI.Pages.Work
{
    public partial class ListTreatmentWorkComponent
    {
        [Inject]
        public ITreatmentService TreatmentService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public string ArableLandName { get; set; } = default!;

        [Parameter]
        public int SizeInDecar { get; set; }

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        private DynamicDataGridModel<GetTreatmentModel> DataGrid { get; set; } = default!;

        [Inject]
        public IState<SelectedWorkingSeasonState> SelectedWorkingSeasonState { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.TreatmentType), "Тип", filterable : true),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleName), "Артикул", filterable : true),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.ArticleQuantity), "Кол. на декар", "{0:n2} кг/л."),
                new DynamicDataGridColumnModel(nameof(GetTreatmentModel.SumArticleQuantity), "Общо кол.", "{0:n2} кг/л.")
            };
            DataGrid = new DynamicDataGridModel<GetTreatmentModel>(await TreatmentService.List(SeedingId), columns, "Третиране")
                .WithAdd(async () => await AddTreatment())
                .WithEdit(async (x) => await EditTreatment(x))
                .WithDelete(async (x) => await DeleteTreatment(x))
                .WithPaging()
                .WithFiltering()
                .WithSorting()
                .WithResizable();

            SelectedWorkingSeasonState.StateChanged += async (sender, state) => await ReLoadData();
        }

        public async Task AddTreatment()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsTreatmentDialog>(
                $"Добавяне на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await ReLoadData();
            }
        }

        public async Task EditTreatment(int treatmentId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsTreatmentDialog>($"Редактиране на Третиране за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "TreatmentId", treatmentId } },
              options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                await ReLoadData();
            }
        }

        public async Task DeleteTreatment(int treatmentId)
        {
            if (await DialogService.ShowDeleteDialog(treatmentId) == true)
            {
                if (await TreatmentService.Delete(treatmentId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != treatmentId));
                    StateHasChanged();
                }
            }
        }

        public async Task ReLoadData()
        {
            DataGrid.UpdateData(await TreatmentService.List(SeedingId));
            this.StateHasChanged();
        }
    }
}
