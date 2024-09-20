﻿using Athena.Application.RepositoryInterface;
using Athena.Domain.Entities;
using Athena.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Athena.Infrastructure.Repository
{
    public class DishTemperatureRepository: IDishTemperatureRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishTemperatureRepository> _logger;

        public DishTemperatureRepository(ILogger<DishTemperatureRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishTemperatureEntity>> GetDishTemperatureTypes()
        {
            return await _athenaDbcontext.DishTemperatureEntity.Where(d => d.ActiveStatus == true).ToListAsync();
        }
    }
}
