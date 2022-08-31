using Application.Models.PerformedWorks;
using Application.Models.Seedings;
using Application.Models.Тreatments;

namespace Application.Services.Seedings
{
    public interface ISeedingService
    {
        Task<GetSeedingModel> GetSeeding(int seasonId, int arableLandId);

        Task AddSeeding(AddSeedingModel seedingModel);
    }
}
