using Microsoft.AspNetCore.Components;
using Radzen;
using WebUI.Services.Article;
using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;
using WebUI.ServicesModel.Enum;

namespace WebUI.Pages.Articles
{
    public partial class DetailsArticle
    {
        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Parameter]
        public int ArticleId { get; set; }

        [Parameter]
        public ArticleType ArticleType { get; set; }

        public ArticleDetailsModel Article { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<SelectionListModel> ArticleTypes { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {
            ArticleTypes = await ArticleService.GetArticlesType();

            if (ArticleId == 0)
            {
                Article = new ArticleDetailsModel((int)ArticleType);
            }
            else
            {
                Article = await ArticleService.Get(ArticleId);
            }
        }

        public void OnDropDownChange(object value)
        {
            Article.ArticleType = (int)value;
        }

        public void OnClose()
        {
            DialogService.Close(false);
        }

        protected async Task OnSubmit(ArticleDetailsModel article)
        {
            bool addIsSuccess = false;

            if (article.Id == 0)
            {
                addIsSuccess = await ArticleService.Add(Article);
            }
            else
            {
                addIsSuccess = await ArticleService.Update(Article);
            }
            DialogService.Close(addIsSuccess);
        }
    }
}
