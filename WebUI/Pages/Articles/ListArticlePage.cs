using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services;
using WebUI.ServicesModel.Article;

namespace WebUI.Pages.Articles
{
    public partial class ListArticlePage
    {
        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<ListArticleModel> Articles { get; set; } = default!;

        public List<string> Headers => new List<string>() { "Ид", "Име", "Тип" };


        protected override async Task OnInitializedAsync()
        {
            Articles = await ArticleService.List();
        }


        public async Task EditArticle(int articleId)
        {
            await DialogService.OpenAsync<DetailsArticlePage>($"Артикул {articleId}",
              new Dictionary<string, object>() { { "ArticleId", articleId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            Articles = await ArticleService.List();
        }

        public async Task AddArticle()
        {
            await DialogService.OpenAsync<DetailsArticlePage>($"Артикул",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            Articles = await ArticleService.List();
        }

        public async Task DeleteArticle(int articleId)
        {
            await DialogService.OpenAsync<DetailsArticlePage>($"Артикул",
              options: new DialogOptions() { Width = "700px", Height = "570px" });
            Articles = await ArticleService.List();
        }

    }
}
