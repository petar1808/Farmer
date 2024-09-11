using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Extensions;
using WebUI.Services.Identity;
using WebUI.ServicesModel.Identity;

namespace WebUI.Pages.Identity.ListUsers
{
    public partial class ListUserPage
    {
        [Inject]
        public IIdentityService IdentityService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public DynamicDataGridModel<ListUserModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListUserModel.UserEmail), "Имейл"),
                new DynamicDataGridColumnModel(nameof(ListUserModel.FirstName), "Име"),
                new DynamicDataGridColumnModel(nameof(ListUserModel.LastName), "Фамилия"),
            };
            DataGrid = new DynamicDataGridModel<ListUserModel>(
                    await IdentityService.ListUser(),
                    columns,
                    "Потребители")
                .WithAdd(async () => await CreateUser())
                .WithPaging()
                .WithSorting();
        }

        public async Task CreateUser()
        {
            var dialogResult = await DialogService.OpenAsync<CreateUser>($"Добавяне на Потребител",
              options: DialogHelper.GetCommonDialogOptions());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await IdentityService.ListUser());
                this.StateHasChanged();
            }
        }
    }
}
