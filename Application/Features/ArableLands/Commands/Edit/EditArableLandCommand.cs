using Application.Models;
using Application.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ArableLands.Commands.Edit
{
    public class EditArableLandCommand : CommonArableLandInputComandModel, IRequest<Result>
    {
        public int Id { get; set; }

        public class EditArableLandCommandHandler : IRequestHandler<EditArableLandCommand, Result>
        {
            private readonly IFarmerDbContext farmerDbContext;

            public EditArableLandCommandHandler(IFarmerDbContext farmerDbContext)
            {
                this.farmerDbContext = farmerDbContext;
            }

            public async Task<Result> Handle(
                EditArableLandCommand request,
                CancellationToken cancellationToken)
            {
                var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

                if (arableLand == null)
                {
                    return $"Земя с Ид: {request.Id} не съществува!";
                }

                if (request.Name != arableLand.Name)
                {
                    var arableLandUnique = await farmerDbContext
                    .ArableLands
                    .AnyAsync(x => x.Name == request.Name, cancellationToken);

                    if (arableLandUnique)
                    {
                        return "Има създадена земя със същото име";
                    }
                }

                arableLand
                    .UpdateName(request.Name)
                    .UpdateSizeInDecar(request.SizeInDecar);

                farmerDbContext.Update(arableLand);
                await farmerDbContext.SaveChangesAsync(cancellationToken);

                return Result.Success;
            }
        }
    }
}
