using Application.Exceptions;
using Application.Models.Тreatments;
using AutoMapper;
using Domain.Enum;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Treatments
{
    public class ТreatmentService : IТreatmentService
    {
        private readonly IFarmerDbContext farmerDbContext;
        private readonly IMapper mapper;

        public ТreatmentService(IFarmerDbContext farmerDbContext, IMapper mapper)
        {
            this.farmerDbContext = farmerDbContext;
            this.mapper = mapper;
        }
        public async Task<List<ListТreatmentModel>> List(int seedingId)
        {
            var treatment = await farmerDbContext
                .Treatments
                .Include(x => x.Article)
                .Where(x => x.SeedingId == seedingId)
                .ToListAsync();

            if (treatment == null)
            {
                throw new BadRequestExeption($"Treatment with Id: {seedingId}, don't exist");
            }

            var result = mapper.Map<List<ListТreatmentModel>>(treatment);

            return result;
        }

        public async Task Add(DateTime date,
            ТreatmentType treatmentType,
            int? amountOfFuel,
            int? fuelPrice,
            int articleId,
            int articleQuantity,
            int seedingId,
            int articlePrice)
        {
            var treatment = new Treatment(date,
                treatmentType,
                amountOfFuel,
                fuelPrice,
                articleId,
                articleQuantity,
                seedingId,
                articlePrice);

            await farmerDbContext.Treatments.AddAsync(treatment);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var treatment = await farmerDbContext
                .Treatments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treatment == null)
            {
                throw new BadRequestExeption($"Treatment with Id: {id}, don't exist");
            }

            farmerDbContext.Remove(treatment);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task Edit(EditТreatmentModel editModel)
        {
            var treatment = await farmerDbContext
              .Treatments
              .FirstOrDefaultAsync(x => x.Id == editModel.Id);

            if (treatment == null)
            {
                throw new BadRequestExeption($"Treatment with Id: {editModel.Id}, don't exist");
            }

            treatment
                .UpdateDate(editModel.Date)
                .UpdateТreatmentType(editModel.ТreatmentType)
                .UpdateAmountOfFuel(editModel.AmountOfFuel)
                .UpdateFuelPrice(editModel.FuelPrice)
                .UpdateArticle(editModel.ArticleId)
                .UpdateArticleQuantity(editModel.ArticleQuantity)
                .UpdateArticlePrice(editModel.ArticlePrice);

            farmerDbContext.Update(treatment);
            await farmerDbContext.SaveChangesAsync();
        }

        public async Task<GetTreatmentModel> Get(int id)
        {
            var treatment = await farmerDbContext
                .Treatments
                .Include(x => x.Article)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treatment == null)
            {
                throw new BadRequestExeption($"Treatment with Id: {id}, don't exist");
            }

            var result = mapper.Map<GetTreatmentModel>(treatment);

            return result;
        }
    }
}
