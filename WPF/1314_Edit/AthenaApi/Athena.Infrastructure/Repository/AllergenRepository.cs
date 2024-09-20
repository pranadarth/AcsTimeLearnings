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
    public class AllergenRepository : IAllergenRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<AllergenRepository> _logger;

        public AllergenRepository(ILogger<AllergenRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<AllergenEntity>> GetAll()
        {
            return await _athenaDbcontext.AllergenEntity.Where(c => c.ActiveStatus == true).ToListAsync();
        }
    }
}
