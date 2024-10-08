﻿using Fluxor;
using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Pages.Seedings.Dialogs;
using WebUI.Services.Seeding;
using WebUI.Services.Subsidies;
using WebUI.ServicesModel.Subsidies;
using WebUI.Store;

namespace WebUI.Pages.Seedings
{
    public partial class ListSubsidy
    {
        [Inject]
        public ISubsidyService SubsidyService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Inject]
        public IDispatcher Dispatcher { get; set; } = default!;

        [Inject]
        public ISeedingService SeedingService { get; set; } = default!;

        [Parameter]
        public string ArableLandName { get; set; } = default!;

        [Parameter]
        public int SizeInDecar { get; set; }

        [Parameter]
        public int SeedingId { get; set; }

        public DynamicDataGridModel<SubsidiesModel> DataGrid { get; set; } = default!;

        protected async override Task OnParametersSetAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(SubsidiesModel.Date), "Дата", "{0:dd/MM/yy}"),
                new DynamicDataGridColumnModel(nameof(SubsidiesModel.Income), "Приход", "{0:n2} лв.")
            };
            DataGrid = new DynamicDataGridModel<SubsidiesModel>(await SubsidyService.List(SeedingId), columns, "Субсидии")
                .WithAdd(async () => await AddSubsidy())
                .WithEdit(async (x) => await EditSubsidy(x))
                .WithDelete(async (x) => await DeleteSubsidy(x))
                .WithPaging()
                .WithSorting();
        }

        public async Task AddSubsidy()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>($"Добавяне на субсидия за земя: {ArableLandName}-{SizeInDecar} декара",
                new Dictionary<string, object>() { { "SeedingId", SeedingId } },
                options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await SubsidyService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task EditSubsidy(int subsidyId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsSubsidyDialog>($"Редактиране на субсидия за земя: {ArableLandName}-{SizeInDecar} декара",
              new Dictionary<string, object>() { { "SubsidyId", subsidyId } },
              options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await SubsidyService.List(SeedingId));
                await UpdateArableLandBalance(this.SeedingId);
                this.StateHasChanged();
            }
        }

        public async Task DeleteSubsidy(int subsidyId)
        {
            if (await DialogService.ShowDeleteDialog(subsidyId) == true)
            {
                if (await this.SubsidyService.Delete(subsidyId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(x => x.Id != subsidyId));
                    await UpdateArableLandBalance(this.SeedingId);
                    this.StateHasChanged();
                }
            }
        }

        private async Task UpdateArableLandBalance(int seedingId)
        {
            this.Dispatcher.Dispatch(
                new UpdateSeedingArableLandBalance(await SeedingService.GetArableLandBalance(seedingId))
                );
        }
    }
}
