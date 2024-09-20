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
    public class DishFoodTypeRepository : IDishFoodTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishFoodTypeRepository> _logger;

        public DishFoodTypeRepository(ILogger<DishFoodTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishFoodTypeEntity>> GetDishFoodTypes()
        {
            return await _athenaDbcontext.DishFoodTypeEntity.Where(x => x.ActiveStatus == true).ToListAsync();
        }
    }
}
