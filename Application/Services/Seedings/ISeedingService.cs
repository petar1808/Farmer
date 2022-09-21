using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Тreatments;

namespace Application.Services.Seedings
{
    public interface ISeedingService
    {
        Task<GetSeedingSummaryModel> GetSeedingSummary(int seedingId);

        Task AddSeeding(AddSeedingModel seedingModel);

        Task<List<SownArableLandModel>> SownArableLands(int seasonId);

        Task UpdateSeedingSummary(UpdateSeedingSummaryModel updateModel, int seedingId);

    }
}
