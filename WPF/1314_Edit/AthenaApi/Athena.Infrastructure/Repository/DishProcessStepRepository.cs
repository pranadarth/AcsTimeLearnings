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
    public class DishProcessStepRepository : IDishProcessStepRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishProcessStepRepository> _logger;

        public DishProcessStepRepository(ILogger<DishProcessStepRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishProcessStepEntity>> GetPreparationProcessSteps()
        {
            return await _athenaDbcontext.DishProcessStepEntity.Where(d => d.ActiveStatus == true).ToListAsync();
        }
    }
}
