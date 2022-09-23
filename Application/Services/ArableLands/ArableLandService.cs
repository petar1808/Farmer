using Application.Exceptions;
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

        public async Task Add(AddArableLandModel arableLandModel)
        {
            var arableLand = new ArableLand(arableLandModel.Name, arableLandModel.SizeInDecar);

            await farmerDbContext.ArableLands.AddAsync(arableLand);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(EditArableLandModel arableLandModel)
        {
            var arableLand = await farmerDbContext
                .ArableLands
                .FirstOrDefaultAsync(x => x.Id == arableLandModel.Id);

            if (arableLand == null)
            {
                throw new BadRequestExeption($"Arable land with Id: {arableLandModel.Id}, don't exist");
            }

            arableLand
                .UpdateName(arableLandModel.Name)
                .UpdateSizeInDecar(arableLandModel.SizeInDecar);

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
                throw new BadRequestExeption($"Arable land with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetAreableLandModel>(arableLand);
            return result;
        }

        public async Task<List<GetAreableLandModel>> List()
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
                throw new BadRequestExeption($"Arable land with Id: {id}, don't exist");
            }

            farmerDbContext.Remove(arableLand);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<List<SelectionListModel>> ArableLandsSelectionList(
            int seasionId)
        {
            var arableLands = await this.farmerDbContext
                .ArableLands
                .Select(x => new SelectionListModel(x.Id, x.Name))
                .ToListAsync();

            var existingArableLands = await this.farmerDbContext
              .Seedings
              .Where(x => x.WorkingSeasonId == seasionId)
              .Select(x => x.ArableLandId)
              .ToListAsync();

            arableLands = arableLands
                .Where(x => !existingArableLands.Contains(x.Value))
                .ToList();

            return arableLands;
        }
    }
}
