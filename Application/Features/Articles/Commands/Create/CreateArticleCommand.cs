﻿using Application.Models;
using Application.Services;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Articles.Commands.Create
{
    public class CreateArticleCommand : CommonArticleInputComandModel, IRequest<Result>
    {
        public class CreateArticleCommandHandler : IRequestHandler<CreateArticleCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public CreateArticleCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                CreateArticleCommand request,
                CancellationToken cancellationToken)
            {
                var articleUnique = await farmerDbContext
                            .Articles
                            .AnyAsync(
                                x => x.Name == request.Name && x.ArticleType == request.ArticleType, 
                                cancellationToken);

                if (articleUnique)
                {
                    return "Има създаден артикул със същото име и тип";
                }

                var article = new Article(request.Name, request.ArticleType);

                await farmerDbContext.AddAsync(article, cancellationToken);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
