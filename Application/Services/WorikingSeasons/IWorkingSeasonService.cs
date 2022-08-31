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
        Task Add(string name, DateTime? startDate, DateTime? endDate);

        Task Edit(int id, string name, DateTime? startDate, DateTime? endDate);

        Task<GetWorkingSeasonModel> Get(int id);

        Task<List<GetWorkingSeasonModel>> GetAll();

        Task<Dictionary<int, string>> ListSidebarMenuItems();

        Task Delete(int id);

        Task<List<SelectionListModel>> SeasonsSelectionList();
    }
}
