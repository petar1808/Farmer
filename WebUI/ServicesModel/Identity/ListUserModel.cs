using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Identity
{
    public class ListUserModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public string UserEmail { get; set; } = default!;

        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;
    }
}
