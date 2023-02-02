namespace Application.Features.Articles.Queries
{
    public class CommonArticleTypeOutputQueryModel
    {
        public CommonArticleTypeOutputQueryModel(
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
