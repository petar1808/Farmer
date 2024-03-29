﻿using FluentValidation;

namespace Application.Features.PerformedWorks.Commands.Edit
{
    public class EditPerformedWorkCommandValidator : AbstractValidator<EditPerformedWorkCommand>
    {
        public EditPerformedWorkCommandValidator()
        {
            this.RuleFor(x => x.WorkType)
                .IsInEnum()
                .WithMessage("Типът работа е невалиден");

            this.RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Датата е задължителна");

            this.RuleFor(x => x.AmountOfFuel)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Количеството гориво трябва да е положително число");

            this.RuleFor(x => x.FuelPrice)
                .ExclusiveBetween(0m, int.MaxValue)
                .WithMessage("Цената на горивото трябва да е положително число");
        }
    }
}
