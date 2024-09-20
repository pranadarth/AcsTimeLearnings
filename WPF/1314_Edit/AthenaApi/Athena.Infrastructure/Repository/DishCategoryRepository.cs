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
    public class DishCategoryRepository : IDishCategoryRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishCategoryRepository> _logger;

        public DishCategoryRepository(ILogger<DishCategoryRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishCategoryEntity>> GetDishCategories()
        {
            return await _athenaDbcontext.DishCategoryEntity.Where(x => x.ActiveStatus == true).ToListAsync();
        }
    }
}
