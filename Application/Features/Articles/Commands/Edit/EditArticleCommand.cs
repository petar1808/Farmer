using Application.Models;
using Application.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Commands.Edit
{
    public class EditArticleCommand : CommonArticleInputComandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditArticleCommandHandler : IRequestHandler<EditArticleCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditArticleCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditArticleCommand request,
                CancellationToken cancellationToken)
            {
                var article = await farmerDbContext
                .Articles
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (article == null)
                {
                    return $"Артикул с Ид: {request.Id} не съществува!";
                }

                var articleUnique = await farmerDbContext
                    .Articles
                    .AnyAsync(x => x.Name == request.Name && x.ArticleType == request.ArticleType, cancellationToken);

                if (articleUnique)
                {
                    return "Има създаден артикул със същото име и тип";
                }

                article
                    .UpdateName(request.Name)
                    .UpdateArticleType(request.ArticleType);

                farmerDbContext.Update(article);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
