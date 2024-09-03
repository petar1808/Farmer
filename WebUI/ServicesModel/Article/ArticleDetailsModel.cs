namespace WebUI.ServicesModel.Article
{
    public class ArticleDetailsModel : ArticleBaseModel
    {
        public ArticleDetailsModel(int articleType)
        {
            ArticleType = articleType;
        }
        public int ArticleType { get; set; }
    }
}
