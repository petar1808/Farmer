namespace Application.Models
{
    public class SelectionListModel
    {
        public SelectionListModel(
            int value,
            string name)
        {
            Value = value;
            Name = name;
        }
        public int Value { get; set; }
        public string Name { get; set; }
    }
}
