﻿using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.Article;
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
                .WithPaging()
                .WithSorting();
        }

        public async Task<bool> DeleteArticleFunction(int articleId)
        {
            return await this.ArticleService.Delete(articleId);
        }

        public async Task AddArticle()
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArticle>($"Добавяне на Артикул",
                options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("300px").WithWidth("600px"));

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArticleService.List());
                this.StateHasChanged();
            }
        }
        public async Task EditArticle(int articleId)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArticle>($"Редактиране на Артикул",
              new Dictionary<string, object>() { { "ArticleId", articleId } },
              options: DialogOptionsHelper.GetCommonDialogOptions().WithHeight("300px").WithWidth("600px"));

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArticleService.List());
                this.StateHasChanged();
            }
        }

        public async Task DeleteArticle(int articleId)
        {
            Func<int, Task<bool>> deleteFunction = (id) =>
            {
                var funcResult = DeleteArticleFunction(articleId);
                return funcResult;
            };

            var deleteModel = new DeleteModalModel(articleId, deleteFunction);
            var dialogResult = await DialogService.OpenAsync<DeleteModal>($"Изтриване на Артикул",
              new Dictionary<string, object>()
              {
                    { "ModelInput", deleteModel }
              },
              options: DialogOptionsHelper.GetDeleteDialogDefaultOptions().WithDefaultSize());

            if (dialogResult == true)
            {
                DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != articleId));

                this.StateHasChanged();
            }
        }
    }
}
