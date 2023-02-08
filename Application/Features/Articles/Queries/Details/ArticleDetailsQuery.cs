using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Queries.Details
{
    public class ArticleDetailsQuery : IRequest<Result<ArticleDetailsQueryOutputModel>>
    {
        public int Id { get; set; }

        public class ArticleDetailsQueryHandler : IRequestHandler<ArticleDetailsQuery, Result<ArticleDetailsQueryOutputModel>>
        {
            private readonly IFarmerDbContext farmerDbContext;
            private readonly IMapper mapper;

            public ArticleDetailsQueryHandler(IFarmerDbContext farmerDbContext, IMapper mapper)
            {
                this.farmerDbContext = farmerDbContext;
                this.mapper = mapper;
            }

            public async Task<Result<ArticleDetailsQueryOutputModel>> Handle(
                ArticleDetailsQuery request, 
                CancellationToken cancellationToken)
            {
                var article = farmerDbContext
                        .Articles
                        .AsQueryable();

                var result = await mapper.ProjectTo<ArticleDetailsQueryOutputModel>(article)
                    .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (result == null)
                {
                    return $"Артикул с Ид: {request.Id} не съществува!";
                }

                return result;
            }
        }
    }
}
