﻿using Application.Models;
using Application.Models.Тreatments;
using AutoMapper;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Result<List<ListТreatmentModel>>> List(int seedingId)
        {
            var seeding = await farmerDbContext
                .Seedings
                .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var treatment = farmerDbContext
                .Treatments
                .Where(x => x.SeedingId == seedingId)
                .AsQueryable();

            var result = await mapper.ProjectTo<ListТreatmentModel>(treatment).ToListAsync();

            return result;
        }

        public async Task<Result> Add(AddТreatmentModel treatmentModel, int seedingId)
        {
            var seeding = await farmerDbContext
               .Seedings
               .AnyAsync(x => x.Id == seedingId);

            if (!seeding)
            {
                return $"Сеитба с Ид: {seedingId} не съществува!";
            }

            var article = await farmerDbContext
                .Articles
                .AnyAsync(x => x.Id == treatmentModel.ArticleId);

            if (!article)
            {
                return $"Артикул с Ид: {treatmentModel.ArticleId} не съществува!";
            }

            var treatment = new Treatment(treatmentModel.Date,
                treatmentModel.TreatmentType,
                treatmentModel.AmountOfFuel,
                treatmentModel.FuelPrice,
                treatmentModel.ArticleId,
                treatmentModel.ArticleQuantity,
                seedingId,
                treatmentModel.ArticlePrice);

            await farmerDbContext.AddAsync(treatment);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Delete(int id)
        {
            var treatment = await farmerDbContext
                .Treatments
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treatment == null)
            {
                return $"Третиране с Ид: {id} не съществува!";
            }

            farmerDbContext.Remove(treatment);
            await farmerDbContext.SaveChangesAsync();

            return Result.Success;
        }

        public async Task<Result> Edit(EditТreatmentModel treatmentModel)
        {
            var article = await farmerDbContext
                .Articles
                .AnyAsync(x => x.Id == treatmentModel.ArticleId);

            if (!article)
            {
                return $"Артикул с Ид: {treatmentModel.ArticleId} не съществува!";
            }

            var treatment = await farmerDbContext
              .Treatments
              .FirstOrDefaultAsync(x => x.Id == treatmentModel.Id);

            if (treatment == null)
            {
                return $"Третиране с Ид: {treatmentModel.Id} не съществува!";
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

            return Result.Success;
        }

        public async Task<Result<GetTreatmentModel>> Get(int id)
        {
            var treatment = farmerDbContext
                .Treatments
                .AsQueryable();

            var result = await mapper.ProjectTo<GetTreatmentModel>(treatment).FirstOrDefaultAsync(x => x.Id == id); ;

            if (result == null)
            {
                return $"Третиране с Ид: {id} не съществува!";
            }

            return result;
        }
    }
}
