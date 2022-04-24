using Application.Services.ArableLands;
using Application.Services.Articles;
using Domain.Enum;
using Moq;

namespace FarmerWeb.Test.Mocks
{
    public class ArticleServiceMock
    {
        public static IArticleService InstanceEdit
        {
            get
            {
                var articleServiceMock = new Mock<IArticleService>();

                articleServiceMock
                    .Setup(a => a.Edit(1,"слънчоглед",ArticleType.Preparations));


                return articleServiceMock.Object;
            }
        }

        public static IArticleService InstanceDelete
        {
            get
            {
                var articleServiceMock = new Mock<IArticleService>();

                articleServiceMock
                    .Setup(a => a.Delete(1));


                return articleServiceMock.Object;
            }
        }

        public static IArticleService InstanceGet
        {
            get
            {
                var articleServiceMock = new Mock<IArticleService>();

                articleServiceMock
                    .Setup(a => a.Get(1));


                return articleServiceMock.Object;
            }
        }


        public static IArticleService InstanceAdd
        {
            get
            {
                var articleServiceMock = new Mock<IArticleService>();

                articleServiceMock
                    .Setup(a => a.Add("пшеница", ArticleType.Seeds));


                return articleServiceMock.Object;
            }
        }
    }
}
