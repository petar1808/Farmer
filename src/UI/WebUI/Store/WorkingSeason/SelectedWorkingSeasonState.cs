namespace WebUI.Store.WorkingSeason
{
    public class SelectedWorkingSeasonState
    {
        public int WorkingSeasonId { get; }

        public string Name { get; }

        public SelectedWorkingSeasonState(int workingSeasonId, string name)
        {
            WorkingSeasonId = workingSeasonId;
            Name = name;
        }
    }
}
