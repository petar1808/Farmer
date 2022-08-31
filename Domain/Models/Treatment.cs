﻿using Domain.Common;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Treatment : Entity<int>
    {
        public Treatment(DateTime date,
            ТreatmentType тreatmentType, 
            int? amountOfFuel,
            int? fuelPrice,
            int articleId,
            int articleQuantity,
            int seedingId,
            int articlePrice)
            :this(date, тreatmentType, articleId, articleQuantity, seedingId,articlePrice)
        {
            AmountOfFuel = amountOfFuel;
            FuelPrice = fuelPrice;
        }

        public Treatment(DateTime date,
            ТreatmentType тreatmentType,
            int articleId,
            int articleQuantity,
            int seedingId,
            int articlePrice)
        {
            Date = date;
            ТreatmentType = тreatmentType;
            ArticleId = articleId;
            ArticleQuantity = articleQuantity;
            SeedingId = seedingId;
            ArticlePrice = articlePrice;
        }

        public DateTime Date { get; private set; }

        public ТreatmentType ТreatmentType { get; private set; }

        public int? AmountOfFuel { get; private set; }

        public int? FuelPrice { get; private set; }

        public int ArticleId { get; private set; }

        public Article Article { get;  } = default!;

        public int ArticleQuantity { get; private set; }

        public int ArticlePrice { get; private set; }

        public int SeedingId { get; private set; }

        public Seeding Seeding { get; } = default!;


        public Treatment UpdateТreatmentType(ТreatmentType treatmentType)
        {
            this.ТreatmentType = treatmentType;
            return this;
        }

        public Treatment UpdateDate(DateTime date)
        {
            this.Date = date;
            return this;
        }

        public Treatment UpdateAmountOfFuel(int? amountOfFuel)
        {
            this.AmountOfFuel = amountOfFuel;
            return this;
        }

        public Treatment UpdateFuelPrice(int? fuelPrice)
        {
            this.FuelPrice = fuelPrice;
            return this;
        }

        public Treatment UpdateArticle(int articleId)
        {
            this.ArticleId = articleId;
            return this;
        }

        public Treatment UpdateArticleQuantity(int articleQuantity)
        {
            this.ArticleQuantity = articleQuantity;
            return this;
        }

        public Treatment UpdateArticlePrice(int articlePrice)
        {
            this.ArticlePrice = articlePrice;
            return this;
        }
    }
}
