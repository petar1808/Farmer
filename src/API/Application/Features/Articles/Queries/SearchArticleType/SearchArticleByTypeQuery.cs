using Application.Models;
using Application.Services;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Queries.SearchArticleType
{
    public class SearchArticleByTypeQuery : IRequest<Result<List<CommonArticleTypeOutputQueryModel>>>
    {
        public ArticleType Type { get; set; }

        public class SearchArticleByTypeQueryHandler : IRequestHandler<SearchArticleByTypeQuery, Result<List<CommonArticleTypeOutputQueryModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public SearchArticleByTypeQueryHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result<List<CommonArticleTypeOutputQueryModel>>> Handle(
                SearchArticleByTypeQuery request,
                CancellationToken cancellationToken)
            {
                var articles = await farmerDbContext
                    .Articles
                    .Where(x => x.ArticleType == request.Type)
                    .Select(x => new CommonArticleTypeOutputQueryModel(x.Id, x.Name))
                    .ToListAsync(cancellationToken);

                return articles;
            }
        }
    }
}
