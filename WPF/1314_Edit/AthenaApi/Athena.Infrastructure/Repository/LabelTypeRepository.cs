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
    public class LabelTypeRepository : ILabelTypeRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<LabelTypeRepository> _logger;

        public LabelTypeRepository(ILogger<LabelTypeRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<LabelTypeEntity>> GetLabelTypes()
        {
            return await _athenaDbcontext.LabelTypeEntity.Where(l => l.ActiveStatus == true).ToListAsync();
        }
    }
}
