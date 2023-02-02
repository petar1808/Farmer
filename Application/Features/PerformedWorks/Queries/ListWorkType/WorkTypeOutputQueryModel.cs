namespace Application.Features.PerformedWorks.Queries.ListWorkType
{
    public class WorkTypeOutputQueryModel
    {
        public WorkTypeOutputQueryModel(
           int value,
           string name)
        {
            this.Value = value;
            this.Name = name;
        }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
