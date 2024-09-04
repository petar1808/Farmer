﻿using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Components.DataGrid;
using WebUI.Components.DeleteModal;
using WebUI.Extensions;
using WebUI.Services.Article;
using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Enum;

namespace WebUI.Pages.Articles
{
    public partial class ListArticlePage
    {
        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        [Parameter]
        public ArticleType ArticleType { get; set; } 

        public DynamicDataGridModel<ListArticleModel> DataGrid { get; set; } = default!;

        // Track the current ArticleType to detect changes
        private ArticleType _currentArticleType;

        protected override async Task OnParametersSetAsync()
        {
            if (_currentArticleType != ArticleType)
            {
                _currentArticleType = ArticleType;
                await LoadDataAsync();
            }
        }

        private async Task LoadDataAsync()
        {
            var columns = new List<DynamicDataGridColumnModel>()
            {
                new DynamicDataGridColumnModel(nameof(ListArticleModel.Id), "Ид"),
                new DynamicDataGridColumnModel(nameof(ListArticleModel.Name), "Име")
            };

            DataGrid = new DynamicDataGridModel<ListArticleModel>(
                    await ArticleService.List(ArticleType),
                    columns)
                .WithEdit(async (x) => await EditArticle(x, ArticleType))
                .WithDelete(async (x) => await DeleteArticle(x))
                .WithPaging()
                .WithSorting();

            // Trigger UI re-render
            StateHasChanged();
        }

        public async Task AddArticle(ArticleType articleType)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArticle>($"Добавяне на {this.ArticleType.GetEnumDisplayName()}",
                new Dictionary<string, object>() { { "ArticleType", articleType } });

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArticleService.List(articleType));
                this.StateHasChanged();
            }
        }
        public async Task EditArticle(int articleId, ArticleType articleType)
        {
            var dialogResult = await DialogService.OpenAsync<DetailsArticle>($"Редактиране на {this.ArticleType.GetEnumDisplayName()}",
              new Dictionary<string, object>() { { "ArticleId", articleId } });

            if (dialogResult == true)
            {
                DataGrid.UpdateData(await ArticleService.List(articleType));
                this.StateHasChanged();
            }
        }

        public async Task DeleteArticle(int articleId)
        {
            if (await DialogService.ShowDeleteDialog(articleId) == true)
            {
                if (await this.ArticleService.Delete(articleId))
                {
                    DataGrid.UpdateData(DataGrid.Data.Where(c => c.Id != articleId));
                    this.StateHasChanged();
                }
            }
        }
    }
}
