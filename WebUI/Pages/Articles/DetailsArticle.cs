using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WebUI.Services.Article;
using WebUI.ServicesModel.Article;
using WebUI.ServicesModel.Common;

namespace WebUI.Pages.Articles
{
    public partial class DetailsArticle
    {
        private string StatusClass = default!;
        private string Message = default!;

        [Inject]
        public IArticleService ArticleService { get; set; } = default!;

        [Parameter]
        public int ArticleId { get; set; }

        public ArticleDetailsModel Article { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;

        public List<SelectionListModel> ArticleTypes { get; set; } = new List<SelectionListModel>();

        protected async override Task OnInitializedAsync()
        {

            ArticleTypes = await ArticleService.GetArticlesType();

            if (ArticleId == 0)
            {
                Article = new ArticleDetailsModel();
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

        protected async Task OnSubmit(ArticleDetailsModel article)
        {
            if (article.Id == 0)
            {
                var addIsSuccess = await ArticleService.Add(Article);
               
                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New Article added successfully.";
                    
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new Article. Please try again.";
                }
            }
            else
            {
                await ArticleService.Update(Article);
                StatusClass = "alert-success";
                Message = "Article updated successfully.";
            }
            DialogService.Close(false);
        }
    }
}
