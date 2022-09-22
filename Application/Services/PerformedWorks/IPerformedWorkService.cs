using Application.Models.PerformedWorks;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PerformedWorks
{
    public interface IPerformedWorkService
    {
        Task<List<ListPerformedWorkModel>> List(int seedingId);

        Task<GetPerformedWorkModel> Get(int id);

        Task Edit(EditPerformedWorkModel editModel);

        Task Delete(int id);

        Task Add(AddPerformedWorkModel performedWorkModel, int seedingId);
    }
}
