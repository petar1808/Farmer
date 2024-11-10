using Domain.Enum;

namespace Application.Features.Articles.Commands
{
    public class CommonArticleInputComandModel
    {
        public string Name { get; set; } = default!;

        public ArticleType ArticleType { get; set; }
    }
}
