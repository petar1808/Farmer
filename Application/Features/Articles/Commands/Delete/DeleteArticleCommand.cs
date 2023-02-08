using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Commands.Delete
{
    public class DeleteArticleCommand : IRequest<Result>
    {
        public int Id { get; set; }

        public class DeleteArticleCommandHandler : IRequestHandler<DeleteArticleCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public DeleteArticleCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                DeleteArticleCommand request,
                CancellationToken cancellationToken)
            {
                var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (article == null)
                {
                    return $"Артикул с Ид: {request.Id} не съществува!";
                }

                var seedingArticle = await farmerDbContext
                    .Seedings
                    .AnyAsync(x => x.ArticleId == request.Id, cancellationToken);

                if (seedingArticle)
                {
                    return $"Артикул с Ид: {request.Id} нe може да бъде изтрит, защото е създадена сеитба с този артикул";
                }

                var treatmentArticle = await farmerDbContext
                    .Treatments
                    .AnyAsync(x => x.ArticleId == request.Id, cancellationToken);

                if (treatmentArticle)
                {
                    return $"Артикул с Ид: {request.Id} нe може да бъде изтрит, защото е създанено третиране с този артикул!";
                }

                farmerDbContext.Articles.Remove(article);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
