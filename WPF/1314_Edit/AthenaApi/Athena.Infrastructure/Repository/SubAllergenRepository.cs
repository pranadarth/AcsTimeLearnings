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
    public class SubAllergenRepository : ISubAllergenRepository
    {
        private readonly AthenaDbContext _athenaDbcontext;
        private readonly ILogger<SubAllergenRepository> _logger;

        public SubAllergenRepository(ILogger<SubAllergenRepository> logger, AthenaDbContext athenaDbContext)
        {
            _athenaDbcontext = athenaDbContext;
            _logger = logger;
        }

        public async Task<List<SubAllergensEntity>> GetAll()
        {
            return await _athenaDbcontext.SubAllergensEntity.Where(c => c.ActiveStatus == true).ToListAsync();
        }
    }
}
