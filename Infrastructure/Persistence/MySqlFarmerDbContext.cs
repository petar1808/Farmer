﻿using Application.Services;
using Infrastructure.DbContect;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class MySqlFarmerDbContext : FarmerDbContext
    {
        public MySqlFarmerDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService) : base(options, currentUserService)
        {
        }
    }
}
