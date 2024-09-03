namespace WebUI.Services
{
    public class SelectedWorkingSeasonService
    {
        public SelectedWorkingSeasonService()
        {

        }
        public int SelectedWorkingSeasonId { get; set; } = 0;

        public void ChangeSelectedWorkingSeason(int selectedWorkingSeasonId)
        {
            SelectedWorkingSeasonId = selectedWorkingSeasonId;
        }


        public bool IsDefaultValue()
        {
            return SelectedWorkingSeasonId == 0;
        }
    }
}
