using Application.Models;
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
        Task<Result<List<ListPerformedWorkModel>>> List(int seedingId);

        Task<Result<GetPerformedWorkModel>> Get(int id);

        Task<Result> Edit(EditPerformedWorkModel editModel);

        Task<Result> Delete(int id);

        Task<Result> Add(AddPerformedWorkModel performedWorkModel, int seedingId);
    }
}
