using Application.Models.Seedings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Seedings
{
    public interface ISeedingService
    {
        Task Add(int arableLandId, int workingSeasonId, int articleId);

        Task<List<GetSeedingModel>> List(int seasionId);

        Task Delete(int id);

        Task<GetSeedingModel> Get(int seedingId);

        Task Edit(int seedingId, int arableLandId, int articleId);
    }
}
