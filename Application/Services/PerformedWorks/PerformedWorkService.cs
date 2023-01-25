using Application.Models;
using Application.Models.PerformedWorks;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.PerformedWorks
{
    public class PerformedWorkService : IPerformedWorkService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public PerformedWorkService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> Delete(int id)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (performedWork == null)
            {
                return $"Обработка с Ид: {id} не съществува!";
            }

            farmerDbContext.Remove(performedWork);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Add(AddPerformedWorkModel performedWorkModel, int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var performedWork = new PerformedWork(seedingId,
                performedWorkModel.WorkType,
                performedWorkModel.Date,
                performedWorkModel.FuelPrice,
                performedWorkModel.AmountOfFuel);

            await farmerDbContext.AddAsync(performedWork);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditPerformedWorkModel editModel)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == editModel.Id);

            if (performedWork == null)
            {
                return $"Обработка с Ид: {editModel.Id} не съществува!";
            }

            performedWork
                .UpdateWorkType(editModel.WorkType)
                .UpdateDate(editModel.Date)
                .UpdateAmountOfFuel(editModel.AmountOfFuel)
                .UpdateFuelPrice(editModel.FuelPrice);

            farmerDbContext.Update(performedWork);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<List<ListPerformedWorkModel>>> List(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var performedWork = farmerDbContext
                .PerformedWorks
                .Where(x => x.SeedingId == seedingId)
                .AsQueryable();

            var result = await mapper.ProjectTo<ListPerformedWorkModel>(performedWork).ToListAsync();

            return result;
        }

        public async Task<Result<GetPerformedWorkModel>> Get(int id)
        {
            var performedWork = farmerDbContext
                .PerformedWorks
                .AsQueryable();

            var result = await mapper.ProjectTo<GetPerformedWorkModel>(performedWork).FirstOrDefaultAsync(x => x.Id == id);

            if (result == null)
            {
                return $"Обработка с Ид: {id} не съществува!";
            }

            return result;
        }
    }
}
