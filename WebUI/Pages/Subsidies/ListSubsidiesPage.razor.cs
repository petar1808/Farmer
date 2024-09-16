using Microsoft.AspNetCore.Components;
using Radzen.Blazor;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Subsidies;
using WebUI.Extensions;
using WebUI.Pages.Seedings.Dialogs;

namespace WebUI.Pages.Subsidies
{
    public partial class ListSubsidiesPage
    {
        [Inject]
        public ISubsidyService SubsidyService { get; set; } = default!;

        public DynamicDataGridModel<SubsidiesModel> DataGrid { get; set; } = default!;

        public RadzenDataGrid<SubsidiesModel> grid;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(SubsidiesModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(SubsidiesModel.Income), "Приход", "{0:n2} лв.")
            };
            DataGrid = new DynamicDataGridModel<SubsidiesModel>(
                    await SubsidyService.ListBySeasonId(1),
                    columns,
                    "Земи")
                .WithAdd(async () => await AddSubsidy())
                .WithEdit(async (x) => await EditSubsidy(x))
                .WithDelete(async (x) => await DeleteSubsidy(x))
                .WithPaging()
                .WithSorting();
        }

        public async Task AddSubsidy()
        {
            //var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>($"Добавяне на субсидия за сезон",
            //    options: DialogHelper.GetCommonDialogOptions());

            //if (dialogResult == true)
            //{
            //    DataGrid.UpdateData(await SubsidyService.ListBySeedingId(SeedingId));
            //    await UpdateArableLandBalance(this.SeedingId);
            //    this.StateHasChanged();
            //}
        }

        public async Task EditSubsidy(int subsidyId)
        {
            //var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>($"Редактиране на субсидия за земя: {ArableLandName}-{SizeInDecar} декара",
            //  new Dictionary<string, object>() { { "SubsidyId", subsidyId } },
            //  options: DialogHelper.GetCommonDialogOptions());

            //if (dialogResult == true)
            //{
            //    DataGrid.UpdateData(await SubsidyService.ListBySeedingId(SeedingId));
            //    await UpdateArableLandBalance(this.SeedingId);
            //    this.StateHasChanged();
            //}
        }

        public async Task DeleteSubsidy(int subsidyId)
        {
            //if (await DialogService.ShowDeleteDialog(subsidyId) == true)
            //{
            //    if (await this.SubsidyService.Delete(subsidyId))
            //    {
            //        DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != subsidyId));
            //        await UpdateArableLandBalance(this.SeedingId);
            //        this.StateHasChanged();
            //    }
            //}
        }
    }
}
