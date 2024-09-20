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
    public class DishMealTypeRepository : IDishMealTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishMealTypeRepository> _logger;

        public DishMealTypeRepository(ILogger<DishMealTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishMealTypeEntity>> GetDishMealTypes()
        {
            return await _athenaDbcontext.DishMealTypeEntity.Where(x => x.ActiveStatus == true).ToListAsync();
        }
    }
}
