using Application.Exceptions;
using Application.Models.Common;
using Application.Models.WorkingSeasons;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.WorikingSeasons
{
    public class WorkingSeasonService : IWorkingSeasonService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;
        private readonly SidebarMenuCache sidebarMenuCache;

        public WorkingSeasonService(IFarmerDbContext farmerDbContext, IMapper mapper, SidebarMenuCache sidebarMenuCache)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
            this.sidebarMenuCache = sidebarMenuCache;
        }

        public async Task Add(AddWorkingSeasonModel workingSeasonModel)
        {
            var workingSeason = new WorkingSeason(
                workingSeasonModel.Name,
                workingSeasonModel.StartDate,
                workingSeasonModel.EndDate);

            await this.farmerDbContext.AddAsync(workingSeason);
            await this.farmerDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workingSeason == null)
            {
                throw new BadRequestExeption($"Working season with Id: {id}, don't exist");
            }

            farmerDbContext.WorkingSeasons.Remove(workingSeason);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(EditWorkingSeasonModel workingSeasonModel)
        {
            sidebarMenuCache.Flush();

            var workingSeason = farmerDbContext
                .WorkingSeasons
                .FirstOrDefault(x => x.Id == workingSeasonModel.Id);

            if (workingSeason == null)
            {
                throw new BadRequestExeption($"Working season with Id: {workingSeasonModel.Id}, don't exist");
            }

            workingSeason
                .UpdateName(workingSeasonModel.Name)
                .UpdateSratDate(workingSeasonModel.StartDate)
                .UpdateEndDate(workingSeasonModel.EndDate);

            this.farmerDbContext.Update(workingSeason);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetWorkingSeasonModel> Get(int id)
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .FirstOrDefaultAsync(x => x.Id == id);

            if (workingSeason == null)
            {
                throw new BadRequestExeption($"Working season with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetWorkingSeasonModel>(workingSeason);
            return result;
        }

        public async Task<List<GetWorkingSeasonModel>> List()
        {
            var workingSeason = await farmerDbContext.WorkingSeasons.ToListAsync();

            var result = mapper.Map<List<GetWorkingSeasonModel>>(workingSeason);
            return result;
        }

        public async Task<List<SelectionListModel>> SeasonsSelectionList()
        {
            var workingSeason = await farmerDbContext
                .WorkingSeasons
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            return workingSeason;
        }
    }
}
