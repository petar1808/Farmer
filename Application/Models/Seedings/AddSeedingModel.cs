﻿using Application.Models.Common;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Seedings
{
    public class AddSeedingModel
    {
        [Required]
        public int ArableLandId { get; init; }

        [Required]
        public int WorkingSeasonId { get; init; }
    }
}
