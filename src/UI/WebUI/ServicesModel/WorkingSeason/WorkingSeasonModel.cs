using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.WorkingSeason
{
    public class WorkingSeasonModel : IDynamicDataGridModel
    {
        public WorkingSeasonModel()
        {
            Name = $"{StartDate.ToString("yyyy")}/{EndDate.ToString("yyyy")}";
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; } = DateTime.Now;

        public DateTime EndDate { get; set; } = DateTime.Now.AddYears(1);
    }
}
