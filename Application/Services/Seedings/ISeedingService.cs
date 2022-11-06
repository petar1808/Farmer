using Application.Models;
using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Тreatments;

namespace Application.Services.Seedings
{
    public interface ISeedingService
    {
        Task<Result<GetSeedingSummaryModel>> GetSeedingSummary(int seedingId);

        Task<Result> AddSeeding(AddSeedingModel seedingModel);

        Task<Result<List<SownArableLandModel>>> SownArableLands(int seasonId);

        Task<Result> UpdateSeedingSummary(UpdateSeedingSummaryModel updateModel, int seedingId);

        Task<Result<GetArableLandBalance>> GetArableLandBalance(int seedingId);
    }
}
