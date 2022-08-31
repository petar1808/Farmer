using WebUI.Components.DataGrid;

namespace WebUI.ServicesModel.Article
{
    public class ListArticleModel : ArticleBaseModel , IDynamicDataGridModel
    {
        public string ArticleType { get; set; } = default!;
    }
}
