using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;

namespace WebUI.Services.Article
{
    public interface IArticleService
    {
        Task<List<ListArticleModel>> List();

        Task<ArticleDetailsModel> Get(int id);

        Task<bool> Update(ArticleDetailsModel article);

        Task<bool> Add(ArticleDetailsModel article);

        Task<List<SelectionListModel>> GetArticlesType();

        Task<List<SelectionListModel>> GetSeeds();

        Task<bool> Delete(int id);
    }
}
