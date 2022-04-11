using Application.Models.Articles;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Articles
{
    public interface IArticleService
    {
        Task Add(string name, ArticleType articleType);

        Task Edit(int id, string name, ArticleType articleType);

        Task<GetArticleModel> Get(int id);

        Task<List<GetArticleModel>> GetAll();

        Task Delete(int id);
    }
}
