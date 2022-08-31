using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.WorkingSeason
{
    public class WorkingSeasonModel : IDynamicDataGridModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
