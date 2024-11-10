using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;

namespace WebUI.Services.Article
{
    public interface IArticleService
    {
        Task<List<ListArticleModel>> List(ArticleType articleType);

        Task<ArticleDetailsModel> Get(int id);

        Task<bool> Update(ArticleDetailsModel article);

        Task<bool> Add(ArticleDetailsModel article);

        Task<List<SelectionListModel>> GetArticlesType();

        Task<bool> Delete(int id);

        Task<List<SelectionListModel>> GetArticles(ArticleType type);
    }
}
