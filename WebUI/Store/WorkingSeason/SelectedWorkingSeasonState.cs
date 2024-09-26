namespace WebUI.Store.WorkingSeason
{
    public class SelectedWorkingSeasonState
    {
        public int WorkingSeasonId { get; }

        public SelectedWorkingSeasonState(int workingSeasonId)
        {
            WorkingSeasonId = workingSeasonId;
        }
    }
}
