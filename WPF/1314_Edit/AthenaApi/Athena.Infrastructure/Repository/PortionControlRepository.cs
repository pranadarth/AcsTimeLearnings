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
    public class PortionControlRepository : IPortionControlRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<PortionControlRepository> _logger;

        public PortionControlRepository(ILogger<PortionControlRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<PortionControlEntity>> GetPortionControl()
        {
            return await _athenaDbcontext.PortionControlEntity.Where(x => x.ActiveStatus == true).ToListAsync();
        }
    }
}
