using Application.Models;
using Application.Models.Articles;
using Application.Models.Common;
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
        Task<Result> Add(AddArticleModel articleModel);

        Task<Result> Edit(EditArticleModel articleModel);

        Task<Result<GetArticleModel>> Get(int id);

        Task<Result<List<ListArticleModel>>> List();

        Task<Result> Delete(int id);

        Task<Result<List<SelectionListModel>>> SeedsArticlesSelectionList();

        Task<Result<List<SelectionListModel>>> TreatmentArticlesSelectionList();
    }
}
