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
    public class CutoffTimeRepository : ICutoffTimeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<CutoffTimeRepository> _logger;

        public CutoffTimeRepository(ILogger<CutoffTimeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<CutoffTimeEntity>> GetCutOffTimings()
        {
            return await _athenaDbcontext.CutoffTimeEntity.Where(c => c.ActiveStatus == true).ToListAsync();
        }
    }
}
