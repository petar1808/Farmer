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

        public WorkingSeasonService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task Add(string name, DateTime? startDate, DateTime? endDate)
        {
            var workingSeason = new WorkingSeason(name, startDate, endDate);

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
                throw new ApplicationException($"Working season with Id: {id}, don't exist");
            }

            farmerDbContext.WorkingSeasons.Remove(workingSeason);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(int id, string name, DateTime? startDate, DateTime? endDate)
        {
            var workingSeason = farmerDbContext
                .WorkingSeasons
                .FirstOrDefault(x => x.Id == id);

            if (workingSeason == null)
            {
                throw new ApplicationException($"Working season with Id: {id}, don't exist");
            }

            workingSeason
                .UpdateName(name)
                .UpdateSratDate(startDate)
                .UpdateEndDate(endDate);

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
                throw new ApplicationException($"Working season with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetWorkingSeasonModel>(workingSeason);
            return result;
        }

        public async Task<List<GetWorkingSeasonModel>> GetAll()
        {
            var workingSeason = await farmerDbContext.WorkingSeasons.ToListAsync();

            var result = mapper.Map<List<GetWorkingSeasonModel>>(workingSeason);
            return result;
        }
    }
}
