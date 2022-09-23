﻿using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.PerformedWork;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.PerformedWork;

namespace WebUI.Pages.Seedings
{
    public partial class DetailsPerformedWorkDialog
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IPerformedWorkService PerformedWorkService { get; set; } = default!;

        [Parameter]
        public int SeedingId { get; set; }

        [Parameter]
        public int PerformedWorkId { get; set; }

        public PerformedWorkDatailsModel PerformedWork { get; set; } = default!;

        [Inject]
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;


        public List<SelectionListModel> WorkTypes { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {

            WorkTypes = await PerformedWorkService.GetWorkTypes();

            if (PerformedWorkId == 0)
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

        protected async Task OnSubmit(PerformedWorkDatailsModel performedWork)
        {
            if (performedWork.Id == 0)
            {
                var addIsSuccess = await PerformedWorkService.Add(PerformedWork,SeedingId);

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
    }
}
