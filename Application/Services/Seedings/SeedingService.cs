using Application.Exceptions;
using Application.Models.Seedings;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Seedings
{
    public class SeedingService : ISeedingService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public SeedingService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }

        public async Task Add(int arableLandId, int workingSeasonId, int articleId)
        {
            var seeding = new Seeding(arableLandId, workingSeasonId, articleId);

            await farmerDbContext.AddAsync(seeding);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var seeding = await farmerDbContext
                    .Seedings
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (seeding == null)
            {
                throw new BadRequestExeption($"Arable land with Id: {id}, don't exist");
            }

            farmerDbContext.Remove(seeding);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(int seedingId, int arableLandId, int articleId)
        {
            var seeding = await farmerDbContext
               .Seedings
               .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                throw new BadRequestExeption($"Seeding with Id: {seedingId}, don't exist");
            }

            seeding
                .UpdateArableLand(arableLandId)
                .UpdateArticle(articleId);

            farmerDbContext.Update(seeding);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Get(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .FirstOrDefaultAsync(x => x.Id == seedingId);

            if (seeding == null)
            {
                throw new BadRequestExeption($"Seeding with Id: {seedingId}, don't exist");
            }
        }

        public List<GetSeedingModel> List(int seasionId)
        {
            var seedings = this.farmerDbContext.Seedings
                .Where(x => x.WorkingSeasonId == seasionId)
                .Include(x => x.ArableLand)
                .Include(x => x.Article)
                .Include(x => x.WorkingSeason)
                .ToList();

            var seedingsMap = mapper.Map<List<GetSeedingModel>>(seedings);
            return seedingsMap;
        }
    }
}
