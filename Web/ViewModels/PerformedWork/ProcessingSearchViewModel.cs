namespace Web.ViewModels.PerformedWork
{
    public class ProcessingSearchViewModel
    {
        public ProcessingSearchViewModel(int seedingId, IEnumerable<PerformedWorkListViewModel> processing)
        {
            SeedingId = seedingId;
            Processings = processing;
        }

        public int SeedingId { get; }

        public IEnumerable<PerformedWorkListViewModel> Processings { get; }
    }
}
