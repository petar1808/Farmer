using Application.Models;
using Application.Models.Seedings;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Application.Services.Seedings
{
    public class SeedingService : ISeedingService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public SeedingService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task<Result> AddSeeding(AddSeedingModel model)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .AnyAsync(x => x.Id == model.ArableLandId);

            if (!arableLand)
            {
                return $"Земя с Ид: {model.ArableLandId} не съществува!";
            }

            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.Id == model.WorkingSeasonId);

            if (!workingSeason)
            {
                return $"Сезон с Ид: {model.WorkingSeasonId} не съществува!";
            }

            var seeding = new Seeding(model.ArableLandId, model.WorkingSeasonId);

            await farmerDbContext.Seedings.AddAsync(seeding);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result<GetSeedingSummaryModel>> GetSeedingSummary(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .Include(x => x.Article)
                .Include(x => x.ArableLand)
                .Include(x => x.Treatments)
                .Include(x => x.PerformedWorks)
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var result = mapper.Map<GetSeedingSummaryModel>(seeding);

            return result;
        }

        public async Task<Result<GetArableLandBalance>> GetArableLandBalance(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .Include(x => x.Article)
                .Include(x => x.ArableLand)
                .Include(x => x.Treatments)
                .Include(x => x.PerformedWorks)
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var result = mapper.Map<GetArableLandBalance>(seeding);

            return result;
        }

        public async Task<Result<List<SownArableLandModel>>> SownArableLands(int seasonId)
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .AnyAsync(x => x.Id == seasonId);

            if (!workingSeason)
            {
                return $"Сезон с Ид: {seasonId} не съществува!";
            }

            var arableLands = await farmerDbContext
                .Seedings
                .Include(x => x.ArableLand)
                .Where(x => x.WorkingSeasonId == seasonId)
                .ToListAsync();

            var result = mapper.Map<List<SownArableLandModel>>(arableLands);

            return result;
        }

        public async Task<Result> UpdateSeedingSummary(UpdateSeedingSummaryModel updateModel, int seedingId)
        {
            var article = await farmerDbContext
                .Articles
                .AnyAsync(x => x.Id == updateModel.ArticleId);

            if (!article && updateModel.ArticleId != null)
            {
                return $"Артикул с Ид: {updateModel.ArticleId} не съществува!";
            }

            var seeding = await farmerDbContext
                .Seedings
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                return $"Сеитба с Ид: {updateModel.Id} не съществува!";
            }

            seeding.UpdateArticle(updateModel.ArticleId)
                   .UpdateSeedsQuantityPerDecare(updateModel.SeedsQuantityPerDecare)
                   .UpdateSeedsPricePerKilogram(updateModel.SeedsPricePerKilogram)
                   .UpdateHarvestedQuantityPerDecare(updateModel.HarvestedQuantityPerDecare)
                   .UpdateHarvestedGrainSellingPricePerKilogram(updateModel.HarvestedGrainSellingPricePerKilogram)
                   .UpdateSubsidies(updateModel.SubsidiesIncome)
                   .UpdateExpensesForHarvesting(updateModel.ExpensesForHarvesting);

            farmerDbContext.Update(seeding);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }
    }
}
