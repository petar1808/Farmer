﻿using FluentValidation;

namespace Application.Features.Seedings.Commands.Edit
{
    public class EditArableLandBalanceCommandValidator : AbstractValidator<EditArableLandBalanceCommand>
    {
        public EditArableLandBalanceCommandValidator()
        {
            this.RuleFor(x => x.SeedsQuantityPerDecare)
                .InclusiveBetween(0m, int.MaxValue)
                .WithMessage("Засято семе на декар трябва да е положително число");

            this.RuleFor(x => x.HarvestedQuantityPerDecare)
                .InclusiveBetween(0, int.MaxValue)
                .WithMessage("Ожънатото количество на декар трябва да е положително число");

            this.RuleFor(x => x.HarvestedGrainSellingPricePerKilogram)
                .InclusiveBetween(0m, int.MaxValue)
                .WithMessage("Продажната цена зърното трябва да е положително число");
        }
    }
}
