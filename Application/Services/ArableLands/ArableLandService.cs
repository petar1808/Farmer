using Application.Models.ArableLands;
using Application.Models.Common;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ArableLands
{
    public class ArableLandService : IArableLandService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;
        public ArableLandService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task Add(string name, int sizeInDecar)
        {
            var arableLand = new ArableLand(name,sizeInDecar);

            await farmerDbContext.ArableLands.AddAsync(arableLand);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(int id, string name, int sizeInDecar)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == id);

            if (arableLand == null)
            {
                throw new ApplicationException($"Arable land with Id: {id}, don't exist");
            }

            arableLand
                .UpdateName(name)
                .UpdateSizeInDecar(sizeInDecar);

            farmerDbContext.Update(arableLand);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetAreableLandModel> Get(int id)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == id);

            if (arableLand == null)
            {
                throw new ApplicationException($"Arable land with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetAreableLandModel>(arableLand);
            return result;
        }

        public async Task<List<GetAreableLandModel>> GetAll()
        {
            var arableLands = await farmerDbContext.ArableLands.ToListAsync();

            var result = mapper.Map<List<GetAreableLandModel>>(arableLands);

            return result;
        }

        public async Task Delete(int id)
        {
            var arableLand = await farmerDbContext
                    .ArableLands
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (arableLand == null)
            {
                throw new ApplicationException($"Arable land with Id: {id}, don't exist");
            }

            farmerDbContext.Remove(arableLand);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<List<SelectionListModel>> ArableLandsSelectionList(
            int seasionId, 
            int? currentArableLandId = null)
        {
            // 2
            var arableLands = await this.farmerDbContext
                .ArableLands
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            // 1
            // 2
            // 3

            var existingArableLands = await this.farmerDbContext
              .Seedings
              .Where(x => x.WorkingSeasonId == seasionId)
              .Select(x => x.ArableLandId)
              .ToListAsync();

            // 1
            // 2

            if (currentArableLandId != null)
            {
                // 1
                existingArableLands = existingArableLands
                    .Where(x => x != currentArableLandId)
                    .ToList();
            }

            arableLands = arableLands
                .Where(x => !existingArableLands.Contains(x.Value))
                .ToList();



            return arableLands;
        }
    }
}
