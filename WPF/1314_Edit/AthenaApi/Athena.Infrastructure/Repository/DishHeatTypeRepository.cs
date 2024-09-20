using Athena.Application.Interface;
using Athena.Application.RepositoryInterface;
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
    public class DishHeatTypeRepository : IDishHeatTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishHeatTypeRepository> _logger;

        public DishHeatTypeRepository(ILogger<DishHeatTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishHeatTypeEntity>> GetDishHeatTypes()
        {
            return await _athenaDbcontext.DishHeatTypeEntity.Where(x => x.ActiveStatus == true).ToListAsync();
        }
    }
}
