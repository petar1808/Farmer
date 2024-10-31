namespace WebUI.Store.WorkingSeason
{
    public class UpdateSelectedWorkingSeasonState
    {
        public int WorkingSeasonId { get; set; }

        public string Name { get; set; }

        public UpdateSelectedWorkingSeasonState(int workingSeasonId, string name)
        {
            WorkingSeasonId = workingSeasonId;
            Name = name;
        }
    }
}
