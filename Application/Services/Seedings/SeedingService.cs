using Application.Exceptions;
using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Тreatments;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task AddSeeding(AddSeedingModel model)
        {
            var seeding = new Seeding(model.ArableLandId, model.WorkingSeasonId);

            await farmerDbContext.Seedings.AddAsync(seeding);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .Include(x => x.Article)
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            var result = mapper.Map<GetSeedingSummaryModel>(seeding);

            return result;
        }

        public async Task<List<SownArableLandModel>> SownArableLands(int seasonId)
        {
            var arableLands = await this.farmerDbContext
                .Seedings
                .Where(x => x.WorkingSeasonId == seasonId)
                .Select(x => new SownArableLandModel(x.Id, x.ArableLand.Name))
                .ToListAsync();

            return arableLands;
        }

        public async Task UpdateSeedingSummary(UpdateSeedingSummaryModel updateModel, int seedingId)
        {
            var seeding = await farmerDbContext.
                Seedings
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                throw new BadRequestExeption($"Seeding with Id: {seedingId}, don't exist");
            }

            seeding.UpdateArticle(updateModel.ArticleId)
                   .UpdateSeedsQuantityPerDecare(updateModel.SeedsQuantityPerDecare)
                   .UpdateSeedsPricePerKilogram(updateModel.SeedsPricePerKilogram)
                   .UpdateHarvestedQuantityPerDecare(updateModel.HarvestedQuantityPerDecare)
                   .UpdateHarvestedGrainSellingPricePerKilogram(updateModel.HarvestedGrainSellingPricePerKilogram)
                   .UpdateSubsidies(updateModel.SubsidiesIncome);

            farmerDbContext.Update(seeding);
            await farmerDbContext.SaveChangesAsync();
        }
    }
}
