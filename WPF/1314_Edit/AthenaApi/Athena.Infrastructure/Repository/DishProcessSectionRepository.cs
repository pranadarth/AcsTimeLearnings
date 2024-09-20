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
    public class DishProcessSectionRepository: IDishProcessSectionRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<DishProcessSectionRepository> _logger;

        public DishProcessSectionRepository(ILogger<DishProcessSectionRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<DishProcessSectionEntity>> GetPreparationProcessSectionTypes()
        {
            return await _athenaDbcontext.DishProcessSectionEntity.Where(d => d.ActiveStatus == true).ToListAsync();
        }
    }
}
