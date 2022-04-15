
namespace Application.Models.Common
{
    public class SelectionListModel
    {
        public SelectionListModel(
            int value,
            string name)
        {
            this.Value = value;
            this.Name = name;
        }
        public int Value { get; }
        public string Name { get; }
    }
}
