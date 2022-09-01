using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
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

        public DynamicDataGridModel<ListArticleModel> DataGrid { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListArticleModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(ListArticleModel.Name), "Име"),
                new DynamicDataGridColumnModel(nameof(ListArticleModel.ArticleType), "Тип"),
            };
            DataGrid = new DynamicDataGridModel<ListArticleModel>(
                    await ArticleService.List(),
                    columns)
                .WithEdit(async (x) => await EditArticle(x))
                .WithDelete(async (x) => await DeleteArticle(x))
                .WithFiltering()
                .WithPaging()
                .WithSorting();
        }

        public async Task DeleteArticleAction(int articleId)
        {
            await this.ArticleService.Delete(articleId);
        }

        public async Task AddArticle()
        {
            await DialogService.OpenAsync<DetailsArticle>($"Артикул",
              options: new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await ArticleService.List());
            this.StateHasChanged();
        }
        public async Task EditArticle(int articleId)
        {
            await DialogService.OpenAsync<DetailsArticle>($"Артикул {articleId}",
              new Dictionary<string, object>() { { "ArticleId", articleId } },
              new DialogOptions() { Width = "700px", Height = "570px" });

            DataGrid.UpdateData(await ArticleService.List());
            this.StateHasChanged();
        }


        public async Task DeleteArticle(int articleId)
        {
            var deleteModel = new DeleteModalModel(articleId, async (id) => await DeleteArticleAction(id));
            await DialogService.OpenAsync<DeleteModal>($"Артикул",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: new DialogOptions() { Width = "500px", Height = "160px" });
            DataGrid.UpdateData(await ArticleService.List());
            this.StateHasChanged();
        }
    }
}
