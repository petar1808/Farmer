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

        public async Task Add(AddТreatmentModel treatmentModel, int seedingId)
        {
            var treatment = new Treatment(treatmentModel.Date,
                treatmentModel.ТreatmentType,
                treatmentModel.AmountOfFuel,
                treatmentModel.FuelPrice,
                treatmentModel.ArticleId,
                treatmentModel.ArticleQuantity,
                seedingId,
                treatmentModel.ArticlePrice);

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

        public async Task Edit(EditТreatmentModel treatmentModel)
        {
            var treatment = await farmerDbContext
              .Treatments
              .FirstOrDefaultAsync(x => x.Id == treatmentModel.Id);

            if (treatment == null)
            {
                throw new BadRequestExeption($"Treatment with Id: {treatmentModel.Id}, don't exist");
            }

            treatment
                .UpdateDate(treatmentModel.Date)
                .UpdateТreatmentType(treatmentModel.TreatmentType)
                .UpdateAmountOfFuel(treatmentModel.AmountOfFuel)
                .UpdateFuelPrice(treatmentModel.FuelPrice)
                .UpdateArticle(treatmentModel.ArticleId)
                .UpdateArticleQuantity(treatmentModel.ArticleQuantity)
                .UpdateArticlePrice(treatmentModel.ArticlePrice);

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
