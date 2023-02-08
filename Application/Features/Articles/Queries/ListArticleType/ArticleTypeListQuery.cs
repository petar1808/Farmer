using Application.Extensions;
using Application.Models;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Queries.ListArticleType
{
    public class ArticleTypeListQuery : IRequest<Result<List<CommonArticleTypeOutputQueryModel>>>
    {
        public class ArticleTypeListQueryHandler : IRequestHandler<ArticleTypeListQuery, Result<List<CommonArticleTypeOutputQueryModel>>>
        {
            public async Task<Result<List<CommonArticleTypeOutputQueryModel>>> Handle(
                ArticleTypeListQuery request,
                CancellationToken cancellationToken)
            {
                var result = await Task.Run(() => 
                {
                    return EnumHelper
                            .GetAllNamesAndValues<ArticleType>()
                            .Select(x => new CommonArticleTypeOutputQueryModel(x.Key, x.Value))
                            .ToList();
                });


                return result;
            }
        }
    }
}
