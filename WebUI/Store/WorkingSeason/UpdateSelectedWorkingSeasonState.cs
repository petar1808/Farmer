namespace WebUI.Store.WorkingSeason
{
    public class UpdateSelectedWorkingSeasonState
    {
        public int SelectedWorkingSeasonId { get; set; }

        public UpdateSelectedWorkingSeasonState(int selectedWorkingSeasonId)
        {
            SelectedWorkingSeasonId = selectedWorkingSeasonId;
        }
    }
}
