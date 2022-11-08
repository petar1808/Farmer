using Application.Models;
using Application.Models.Common;
using Application.Models.WorkingSeasons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorikingSeasons
{
    public interface IWorkingSeasonService
    {
        Task<Result> Add(AddWorkingSeasonModel workingSeasonModel);

        Task<Result> Edit(EditWorkingSeasonModel workingSeasonModel);

        Task<Result<GetWorkingSeasonModel>> Get(int id);

        Task<Result> Delete(int id);

        Task<Result<List<SelectionListModel>>> SeasonsSelectionList();

        Task<Result<List<ListWorkingSeasonBalanceModel>>> ListWorkingSeasonsBalance();
    }
}
