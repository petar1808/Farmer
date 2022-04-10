using Application.Models.ArableLands;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ArableLands
{
    public class ArableLandService : IArableLandService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper _mapper;
        public ArableLandService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            _mapper = mapper;
        }

        public async Task Add(string name, int sizeInDecar)
        {
            var arableLand = new ArableLand(name,sizeInDecar);

            await this.farmerDbContext.AddAsync(arableLand);
            await this.farmerDbContext.SaveChangesAsync();
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

            this.farmerDbContext.Update(arableLand);
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

            var result = this._mapper.Map<GetAreableLandModel>(arableLand);
            return result;
        }

        public async Task<List<GetAreableLandModel>> GetAll()
        {
            var arableLands = await farmerDbContext.ArableLands.ToListAsync();

            var result = this._mapper.Map<List<GetAreableLandModel>>(arableLands);

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

            this.farmerDbContext.Remove(arableLand);
            await this.farmerDbContext.SaveChangesAsync();
        }
    }
}
