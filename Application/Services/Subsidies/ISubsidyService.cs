using Application.Models;
using Application.Models.Subsidies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Subsidies
{
    public interface ISubsidyService
    {
        Task<Result<List<ListSubsidiesModel>>> List(int seedingId);

        Task<Result<GetSubsidyModel>> Get(int id);

        Task<Result> Delete(int id);

        Task<Result> Edit(EditSubsidyModel subsidyModel);

        Task<Result> Add(AddSubsidyModel subsidyModel, int seedingId);
    }
}
