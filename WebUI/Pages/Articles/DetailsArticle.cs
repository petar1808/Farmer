using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using WebUI.Services;
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
        public NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        public DialogService DialogService { get; set; } = default!;


        public List<SelectionListModel> ArticleTypes { get; set; } = new List<SelectionListModel>();

        bool popup;

        protected async override Task OnInitializedAsync()
        {

            ArticleTypes = await ArticleService.GetArticlesType();

            if (ArticleId == 0) //new employee is being created
            {
                //add some defaults
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
            if (article.Id == 0) //new
            {
                var addIsSuccess = await ArticleService.Add(Article);
               
                if (addIsSuccess)
                {
                    StatusClass = "alert-success";
                    Message = "New employee added successfully.";
                    
                }
                else
                {
                    StatusClass = "alert-danger";
                    Message = "Something went wrong adding the new employee. Please try again.";
                }
            }
            else
            {
                await ArticleService.Update(Article);
                StatusClass = "alert-success";
                Message = "Employee updated successfully.";
            }
            DialogService.Close(false);
        }

        void OnDropDownChange(object value, string name)
        {
            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
        }

        protected async Task DeleteEmployee()
        {
            await ArticleService.Delete(Article.Id);

            StatusClass = "alert-success";
            Message = "Deleted successfully";
        }
    }
}
