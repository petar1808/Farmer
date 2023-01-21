using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.ArableLand
{
    public class ArableLandModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public int? SizeInDecar { get; set; }
    }
}
