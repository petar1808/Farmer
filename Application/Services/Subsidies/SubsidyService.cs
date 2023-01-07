using Application.Models;
using Application.Models.Subsidies;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Subsidies
{
    public class SubsidyService : ISubsidyService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;
        public SubsidyService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Add(AddSubsidyModel subsidyModel, int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var subsidy = new Subsidy(seedingId,
                subsidyModel.Income,
                subsidyModel.Date);

            await farmerDbContext.Subsidies.AddAsync(subsidy);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Delete(int id)
        {
            var subsidy = await farmerDbContext
                .Subsidies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subsidy == null)
            {
                return $"Субсидия с Ид: {id} не съществува";
            }

            farmerDbContext.Remove(subsidy);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditSubsidyModel subsidyModel)
        {
            var subsidy = await farmerDbContext
                .Subsidies
                .FirstOrDefaultAsync(x => x.Id == subsidyModel.Id);

            if (subsidy == null)
            {
                return $"Субсидия с Ид: {subsidyModel.Id} не съществува";
            }

            subsidy
                .UpdateIncome(subsidyModel.Income)
                .UpdateDate(subsidyModel.Date);

            farmerDbContext.Update(subsidy);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<GetSubsidyModel>> Get(int id)
        {
            var subsidy = await farmerDbContext
                .Subsidies
                .FirstOrDefaultAsync(x => x.Id == id);

            if (subsidy == null)
            {
                return $"Субсидия с Ид: {id} не съществува";
            }

            var result = mapper.Map<GetSubsidyModel>(subsidy);

            return result;
        }

        public async Task<Result<List<ListSubsidiesModel>>> List(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var subsidy = await farmerDbContext
                .Subsidies
                .Where(x => x.SeedingId == seedingId)
                .ToListAsync();

            var result = mapper.Map<List<ListSubsidiesModel>>(subsidy);

            return result;
        }
    }
}
