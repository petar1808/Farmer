﻿using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.PerformedWorks
{
    public class AddPerformedWorkModel
    {
        [Required(ErrorMessage = "Типът работа е задължителен")]
        [EnumDataType(typeof(WorkType))]
        public WorkType WorkType { get; init; }

        [Required(ErrorMessage = "Датата е задължителна")]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; init; }

        [Required(ErrorMessage = "Количеството гориво е задължително")]
        [Range(0d, int.MaxValue, ErrorMessage = "Количеството гориво трябва да е положително число")]
        public decimal AmountOfFuel { get; init; }

        [Required(ErrorMessage = "Цената на горивото е задължителна")]
        [Range(0d, int.MaxValue, ErrorMessage = "Цената на горивото трябва да е положително число")]
        public decimal FuelPrice { get; init; }
    }
}
