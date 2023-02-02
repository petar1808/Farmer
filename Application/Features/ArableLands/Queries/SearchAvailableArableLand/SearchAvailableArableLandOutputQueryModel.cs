namespace Application.Features.ArableLands.Queries.SearchAvailableArableLand
{
    public class SearchAvailableArableLandOutputQueryModel
    {
        public SearchAvailableArableLandOutputQueryModel(
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
