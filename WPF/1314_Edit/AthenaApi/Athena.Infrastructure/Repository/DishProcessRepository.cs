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
    public class DishProcessRepository : IDishProcessRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishProcessRepository> _logger;

        public DishProcessRepository(ILogger<DishProcessRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishProcessEntity>> GetPreparationProcessTypes()
        {
            return await _athenaDbcontext.DishProcessEntity.Where(d => d.ActiveStatus == true).ToListAsync();
        }
    }
}
