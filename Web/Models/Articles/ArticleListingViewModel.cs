using Domain.Enum;

namespace Web.Models.Articles
{
    public class ArticleListingViewModel
    {
        public int Id { get; init; }

        public string Name { get; init; } = default!;

        public ArticleType ArticleType { get; init; }
    }
}
