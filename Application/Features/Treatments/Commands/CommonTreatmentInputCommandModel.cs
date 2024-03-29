﻿using Domain.Enum;

namespace Application.Features.Treatments.Commands
{
    public class CommonTreatmentInputCommandModel
    {
        public DateTime Date { get; init; }

        public ТreatmentType TreatmentType { get; init; }

        public decimal? AmountOfFuel { get; init; }

        public decimal? FuelPrice { get; init; }

        public int ArticleId { get; init; }

        public decimal ArticleQuantity { get; init; }

        public decimal ArticlePrice { get; init; }
    }
}
