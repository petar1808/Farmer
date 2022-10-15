using System.Text;
using System.Text.Json;
using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;

namespace WebUI.Services.Article
{
    public class ArticleService : IArticleService
    {
        private readonly HttpClient _httpClient;
        public ArticleService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ArticleDetailsModel> Get(int id)
        {
            var result = await JsonSerializer.DeserializeAsync<ArticleDetailsModel>
                (await _httpClient.GetStreamAsync($"api/Articles/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<SelectionListModel>> GetArticlesType()
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
                (await _httpClient.GetStreamAsync($"api/assets/articleTypes"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }

        public async Task<List<ListArticleModel>> List()
        {
            var result = await JsonSerializer.DeserializeAsync<List<ListArticleModel>>
                (await _httpClient.GetStreamAsync($"api/Articles"), new JsonSerializerOptions() { PropertyNameCaseInsensitive= true});

            return result!;
        }

        public async Task<bool> Add(ArticleDetailsModel article)
        {
            var articleJson =
                new StringContent(JsonSerializer.Serialize(article), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/Articles", articleJson);

            return response.IsSuccessStatusCode;
        }

        public async Task Update(ArticleDetailsModel article)
        {
            var articleJson =
                new StringContent(JsonSerializer.Serialize(article), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/Articles", articleJson);
        }

        public async Task Delete(int id)
        {
            await _httpClient.DeleteAsync($"api/Articles/{id}");
        }

        public async Task<List<SelectionListModel>> GetSeeds()
        {
            var result = await JsonSerializer.DeserializeAsync<List<SelectionListModel>>
                (await _httpClient.GetStreamAsync($"api/assets/seeds"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return result!;
        }
    }
}
