﻿using Domain.Common;
using Domain.Enum;

namespace Domain.Models
{
    public class Treatment : Entity<int>, ITenant
    {
        public Treatment(DateTime date,
            ТreatmentType treatmentType,
            decimal? amountOfFuel,
            decimal? fuelPrice,
            int articleId,
            decimal articleQuantity,
            int seedingId,
            decimal articlePrice)
            : this(date, treatmentType, articleId, articleQuantity, seedingId, articlePrice)
        {
            AmountOfFuel = amountOfFuel;
            FuelPrice = fuelPrice;
        }

        public Treatment(DateTime date,
            ТreatmentType treatmentType,
            int articleId,
            decimal articleQuantity,
            int seedingId,
            decimal articlePrice)
        {
            ValidateТreatmentType(treatmentType);
            Date = date;
            TreatmentType = treatmentType;
            ArticleId = articleId;
            ArticleQuantity = articleQuantity;
            SeedingId = seedingId;
            ArticlePrice = articlePrice;
        }

        public DateTime Date { get; private set; }

        public ТreatmentType TreatmentType { get; private set; }

        public decimal? AmountOfFuel { get; private set; }

        public decimal? FuelPrice { get; private set; }

        public int ArticleId { get; private set; }

        public Article Article { get; } = default!;

        public decimal ArticleQuantity { get; private set; }

        public decimal ArticlePrice { get; private set; }

        public int SeedingId { get; private set; }

        public Seeding Seeding { get; } = default!;

        public int TenantId { get; set; }

        #region UpdateMethods
        public Treatment UpdateТreatmentType(ТreatmentType treatmentType)
        {
            ValidateТreatmentType(treatmentType);
            this.TreatmentType = treatmentType;
            return this;
        }

        public Treatment UpdateDate(DateTime date)
        {
            this.Date = date;
            return this;
        }

        public Treatment UpdateAmountOfFuel(decimal? amountOfFuel)
        {
            this.AmountOfFuel = amountOfFuel;
            return this;
        }

        public Treatment UpdateFuelPrice(decimal? fuelPrice)
        {
            this.FuelPrice = fuelPrice;
            return this;
        }

        public Treatment UpdateArticle(int articleId)
        {
            this.ArticleId = articleId;
            return this;
        }

        public Treatment UpdateArticleQuantity(decimal articleQuantity)
        {
            this.ArticleQuantity = articleQuantity;
            return this;
        }

        public Treatment UpdateArticlePrice(decimal articlePrice)
        {
            this.ArticlePrice = articlePrice;
            return this;
        }
        #endregion

        private void ValidateТreatmentType(ТreatmentType type)
            => Guard.Guard.ForValidEnum<ТreatmentType>((int)type, nameof(ТreatmentType));
    }
}
