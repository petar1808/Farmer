using Application.Models;
using Application.Services;
using AutoMapper;
using Domain.Enum;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Queries.List
{
    public class ArticleListQuery : IRequest<Result<List<ArticleListQueryOutputModel>>>
    {
        public ArticleType ArticleType { get; set; }

        public class ArticleListQueryHandler : IRequestHandler<ArticleListQuery, Result<List<ArticleListQueryOutputModel>>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ArticleListQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<List<ArticleListQueryOutputModel>>> Handle(
                ArticleListQuery request,
                CancellationToken cancellationToken)
            {
                var articles = farmerDbContext.Articles.Where(x => x.ArticleType == request.ArticleType).AsQueryable();

                var result = await mapper.ProjectTo<ArticleListQueryOutputModel>(articles)
                    .OrderByDescending(x => x.Id).ToListAsync(cancellationToken);

                return result;
            }
        }

    }
}
