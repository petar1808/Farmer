using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;

namespace WebUI.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly IHttpService _httpService;
        public ArticleService(IHttpService httpService)
        {
            this._httpService = httpService;
        }

        public async Task<ArticleDetailsModel> Get(int id)
        {
            return await _httpService
                .GetAsync<ArticleDetailsModel>($"api/Articles/{id}");
        }

        public async Task<List<SelectionListModel>> GetArticlesType()
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/articleTypes");
        }

        public async Task<List<ListArticleModel>> List()
        {
            return await _httpService
                .GetAsync<List<ListArticleModel>>($"api/articles");
        }

        public async Task<bool> Add(ArticleDetailsModel article)
        {
            return await _httpService
                .PostAsync<bool>("api/Articles", article);
        }

        public async Task<bool> Update(ArticleDetailsModel article)
        {
            return await _httpService
                .PutAsync<bool>("api/Articles", article);
        }

        public async Task<bool> Delete(int id)
        {
            return await _httpService
                 .DeleteAsync<bool>($"api/Articles/{id}");
        }

        public async Task<List<SelectionListModel>> GetArticles(ArticleType type)
        {
            return await _httpService
                .GetAsync<List<SelectionListModel>>($"api/assets/treatment/{(int)type}");
        }
    }
}
