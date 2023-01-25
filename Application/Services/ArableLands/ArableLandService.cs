using Application.Models;
using Application.Models.ArableLands;
using Application.Models.Common;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ArableLands
{
    public class ArableLandService : IArableLandService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;
        public ArableLandService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Add(AddArableLandModel arableLandModel)
        {
            var arableLandUnique = await farmerDbContext
                .ArableLands
                .AnyAsync(x => x.Name == arableLandModel.Name);

            if (arableLandUnique)
            {
                return "Има създадена земя със същото име";
            }

            var arableLand = new ArableLand(arableLandModel.Name, arableLandModel.SizeInDecar);

            await farmerDbContext.AddAsync(arableLand);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditArableLandModel arableLandModel)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == arableLandModel.Id);

            if (arableLand == null)
            {
                return $"Земя с Ид: {arableLandModel.Id} не съществува!";
            }

            var arableLandUnique = await farmerDbContext
                .ArableLands
                .AnyAsync(x => x.Name == arableLandModel.Name);

            if (arableLandUnique)
            {
                return "Има създадена земя със същото име";
            }

            arableLand
                .UpdateName(arableLandModel.Name)
                .UpdateSizeInDecar(arableLandModel.SizeInDecar);

            farmerDbContext.Update(arableLand);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<GetAreableLandModel>> Get(int id)
        {
            var arableLand = farmerDbContext
                .ArableLands
                .AsQueryable();

            var result = await mapper.ProjectTo<GetAreableLandModel>(arableLand).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return $"Земя с Ид: {id} не съществува!";
            }

            return result;
        }

        public async Task<Result<List<GetAreableLandModel>>> List()
        {
            var arableLands = farmerDbContext.ArableLands.AsQueryable();

            var result = await mapper.ProjectTo<GetAreableLandModel>(arableLands).ToListAsync();

            return result;
        }

        public async Task<Result> Delete(int id)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == id);

            if (arableLand == null)
            {
                return $"Земя с Ид: {id} не съществува!";
            }

            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.ArableLandId == id);

            if (seeding)
            {
                return $"Земя с Ид: {id} не може да бъде изтрита, защото имате данни в сеитбата с тази земя!";
            }

            farmerDbContext.Remove(arableLand);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<List<SelectionListModel>>> GetAvailableArableLands(int seasonId)
        {
            var workingseason = await this.farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.Id == seasonId);

            if (!workingseason)
            {
                return $"Сезон с Ид: {seasonId} не съществува!";
            }

            var arableLands = await this.farmerDbContext
                .ArableLands
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            var existingArableLands = await this.farmerDbContext
              .Seedings
              .Where(x => x.WorkingSeasonId == seasonId)
              .Select(x => x.ArableLandId)
              .ToListAsync();

            arableLands = arableLands
                .Where(x => !existingArableLands.Contains(x.Value))
                .ToList();

            return arableLands;
        }
    }
}
