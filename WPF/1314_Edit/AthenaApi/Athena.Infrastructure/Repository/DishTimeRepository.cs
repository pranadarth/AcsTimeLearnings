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
    public class DishTimeRepository : IDishTimeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishTimeRepository> _logger;

        public DishTimeRepository(ILogger<DishTimeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishTimeEntity>> GetDishTimeTypes()
        {
            return await _athenaDbcontext.DishTimeEntity.Where(d => d.ActiveStatus == true).ToListAsync();
        }
    }
}
