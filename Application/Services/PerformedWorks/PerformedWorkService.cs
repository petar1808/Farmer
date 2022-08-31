using Application.Exceptions;
using Application.Models.PerformedWorks;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PerformedWorks
{
    public class PerformedWorkService : IPerformedWorkService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public PerformedWorkService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task Delete(int id)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (performedWork == null)
            {
                throw new BadRequestExeption($"Performed work with Id: {id}, don't exist");
            }

            farmerDbContext.Remove(performedWork);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Add(int seedingId, WorkType workType, DateTime date, int fuelPrice, int amountOfFuel)
        {
            var performedWork = new PerformedWork(seedingId,
                workType,
                date,
                fuelPrice,
                amountOfFuel);

            await farmerDbContext.PerformedWorks.AddAsync(performedWork);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(EditPerformedWorkModel editModel)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == editModel.Id);

            if (performedWork == null)
            {
                throw new BadRequestExeption($"Performed work with Id: {editModel.Id}, don't exist");
            }

            performedWork
                .UpdateWorkType(editModel.WorkType)
                .UpdateDate(editModel.Date)
                .UpdateAmountOfFuel(editModel.AmountOfFuel)
                .UpdateFuelPrice(editModel.FuelPrice);

            farmerDbContext.Update(performedWork);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<List<ListPerformedWorkModel>> List(int seedingId)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .Where(x => x.SeedingId == seedingId)
                .ToListAsync();

            if (performedWork == null)
            {
                throw new BadRequestExeption($"Performed work with Id: {seedingId}, don't exist");
            }

            var result = mapper.Map<List<ListPerformedWorkModel>>(performedWork);

            return result;
        }

        public async Task<GetPerformedWorkModel> Get(int id)
        {
            var performedWork = await farmerDbContext
                .PerformedWorks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (performedWork == null)
            {
                throw new BadRequestExeption($"Performed work with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetPerformedWorkModel>(performedWork);

            return result;
        }
    }
}
