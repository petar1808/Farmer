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
        Task Add(AddWorkingSeasonModel workingSeasonModel);

        Task Edit(EditWorkingSeasonModel workingSeasonModel);

        Task<GetWorkingSeasonModel> Get(int id);

        Task<List<GetWorkingSeasonModel>> List();

        Task Delete(int id);

        Task<List<SelectionListModel>> SeasonsSelectionList();
    }
}
